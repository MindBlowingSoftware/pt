using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Linq;
using PtShared;
using System.Collections.Generic;
using System.Globalization;

namespace PtSync
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Options provided {(args?.Length > 0 ? args[0] : "None")}");


            if (args?.Length > 0 ? args[0] == "super": false) { UpdateSuper(); return; }
            var refreshTimespan = new TimeSpan(4, 0, 0);
            using (var dbContext = new PTContext())
            {
                var program = new Program();
                var tickers = dbContext.Ticker.Where(t => !string.IsNullOrWhiteSpace(t.Url));
                foreach (var ticker in tickers)
                {
                    var lastTickerRecorded = dbContext.TickerHistory
                        .Where(a=>a.SchemeCode == ticker.SchemeCode)
                        .Max(a => a.Daterecorded);

                    if (lastTickerRecorded.HasValue && (DateTime.Now - lastTickerRecorded.Value) < refreshTimespan)
                        continue;
                    var nav = program.GetTickerUpdate(ticker.Url);
                    nav.Wait();

                    if (nav != null)
                    {
                        var tickerHistory = new TickerHistory()
                        {
                            Daterecorded = DateTime.Now,
                            SchemeCode = ticker.SchemeCode,
                            Value = nav.Result
                        };
                        dbContext.TickerHistory.Add(tickerHistory);
                        
                    }
                    var task =  Task.Delay(1000 * 10);
                    task.Wait();
                }
                dbContext.SaveChanges();
            }
                //PTContext.ConnectionString = "Data Source=TRAPPIST-PC\\SQLEXPRESS;Initial Catalog=PT;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    
        }

        private static void UpdateSuper()
        {
            var dir = @"C:\Users\Trappist\Downloads\";
            var files = Directory.GetFiles(dir, "ActivityStatement.*.html");
            using (var dbContext = new PTContext())
            {
                foreach (var file in files)
                {
                    Console.Write(file);
                    string date = file
                        .Replace(dir, "")
                        .Replace("ActivityStatement.", "")
                        .Replace(".html", "");
                    Console.Write(date);

                    var fileContents = File.ReadAllText(file);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(fileContents);
                    var table = doc.GetElementbyId("tblOpenPositions_U1688065Body");
                    var tbodies = table.ChildNodes
                        .Where(a => a.Name == "div").First()
                        .ChildNodes.Where(a => a.Name == "table").First()
                        .ChildNodes.Where(a => a.Name == "tbody");
                    foreach (var tbody in tbodies)
                    {
                        var row = tbody.ChildNodes.Where(a => a.Name == "tr").First();
                        if(row?.Attributes["class"]?.Value == "row-summary no-details")
                        {
                            var tds = row.ChildNodes.Where(a => a.Name == "td").ToList();
                            var ib = new Ib();
                            if(date.Length < 8) date = date + "28";
                            ib.Dateimported = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                            ib.Symbol = tds[0].InnerText;
                            ib.Quantity = Convert.ToInt32(tds[1].InnerText);
                            ib.Mult = tds[2].InnerText;
                            ib.CostPrice = Convert.ToDecimal(tds[3].InnerText);
                            ib.CostBasis = Convert.ToDecimal(tds[4].InnerText);
                            ib.ClosePrice = Convert.ToDecimal(tds[5].InnerText);
                            ib.Value = Convert.ToDecimal(tds[6].InnerText);
                            ib.UnrealizedPL = Convert.ToDecimal(tds[7].InnerText);
                            ib.Code = tds[8].InnerText;
                            dbContext.Ib.Add(ib);
                        }

                        
                    }
                    File.Move(file, @"C:\Data\Imported\" + file.Replace(dir, ""));
                }
                dbContext.SaveChanges();
            }
        }

        private async Task<decimal?> GetTickerUpdate(string url)
        {
            Task<HttpResponseMessage> response;

            using (var client = new HttpClient())
            {
                try
                {
                    response = client.GetAsync(url);
                    response.Wait();
                    if (response != null)
                    {
                        Stream stream = await response.Result.Content.ReadAsStreamAsync();

                        HtmlDocument doc = new HtmlDocument();

                        doc.Load(stream);
                        if(url.Contains("https://www.moneycontrol.com/mutual-funds/nav"))
                        {
                            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//title");//the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 
                            var match = Regex.Split(links[0].InnerText.Split('[')[1], @"[^0-9\.]+")
                                .FirstOrDefault(c => c != "." && c.Trim() != "");
                            if (match != null)
                            {
                                return Convert.ToDecimal(match);
                            }
                            return null;
                        }
                        else if (url.Contains("https://www.moneycontrol.com/india/stockpricequote"))
                        {
                            HtmlNode priceTick = doc.GetElementbyId("Nse_Prc_tick");//the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 
                            var match = priceTick.InnerText;
                            if (match != null)
                            {
                                return Convert.ToDecimal(match);
                            }
                            return null;
                        }
                        else if (url.Contains("https://quotes.wsj.com/etf"))
                        {
                            HtmlNode priceTick = doc.GetElementbyId("quote_val");//the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 
                            var match = priceTick.InnerText;
                            if (match != null)
                            {
                                return Convert.ToDecimal(match);
                            }
                            return null;
                        }

                    }
                    return null;
                }
                catch (Exception ex)
                {

                }
                return null;
            }
            
            
        }
    }
}
