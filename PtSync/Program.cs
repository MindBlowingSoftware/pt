using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Linq;
using PtShared;

namespace PtSync
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var refreshTimespan = new TimeSpan(12, 0, 0);
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
