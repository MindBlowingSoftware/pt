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
                    var lastTickerRecorded = dbContext.TickerHistory.Max(a => a.Daterecorded);

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
                    //Task.Delay(1000 * 60 * 2);
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

                        //HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");//the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 
                        HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//title");//the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 
                        var match = Regex.Split(links[0].InnerText.Split('[')[1], @"[^0-9\.]+").FirstOrDefault(c => c != "." && c.Trim() != "");
                        if (match != null)
                        {
                            return Convert.ToDecimal(match);
                        }
                        return null;
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
