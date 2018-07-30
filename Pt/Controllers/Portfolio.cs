using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pt.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pt.Controllers
{
    [Route("api/[controller]")]
    public class PortfolioController : Controller
    {
        private PTContext _dbContext;
        public PortfolioController(PTContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<controller>
        [HttpGet("[action]")]
        public IEnumerable<PortfolioItem> Get()
        {
            var savings =  _dbContext.Savings;
            var stocks =  _dbContext.Icicinre;
            var mutualFunds =  _dbContext.YesBankWm;
            var mutualFunds2 =  _dbContext.IciciNro;
            var audinr = 50;

            var portfolio = new List<PortfolioItem>();
            foreach(var saving in savings)
            {
                portfolio.Add(new PortfolioItem
                {
                    Amount = Math.Round(saving.Amount / audinr,0),
                    AmountInvested = Math.Round(saving.Amount / audinr,0),
                    Name = saving.Account,
                    Date = saving.Date,
                    Type = "Savings",
                });
            }

            foreach (var stock in stocks)
            {
                portfolio.Add(new PortfolioItem
                {
                    Amount = Math.Round(Convert.ToDecimal(stock.ValueAtMarketPrice) / audinr,0),
                    AmountInvested = Math.Round(Convert.ToDecimal(stock.ValueAtCost) / audinr,0),
                    Name = stock.StockSymbol,
                    Date = stock.Dateimported.Value,
                    Type = "Stocks"
                });
            }

            foreach (var mf in mutualFunds)
            {
                portfolio.Add(new PortfolioItem
                {
                    Amount = Math.Round(Convert.ToDecimal(mf.CurrentValue) / audinr, 0),
                    AmountInvested = Math.Round(Convert.ToDecimal(mf.AmountInvested) / audinr, 0),
                    Name = mf.SchemeCode,
                    Date = Convert.ToDateTime(mf.ValDate),
                    Type = "Mutual Funds"
                });
            }

            foreach (var mf in mutualFunds2)
            {
                portfolio.Add(new PortfolioItem
                {
                    Amount = Math.Round(Convert.ToDecimal(mf.ValueAtNav) / audinr, 0),
                    AmountInvested = Math.Round(Convert.ToDecimal(mf.ValueAtCost) / audinr, 0),
                    Name = mf.Fund,
                    Date = Convert.ToDateTime(mf.LastRecordedNavOn),
                    Type = "Mutual Funds"
                });
            }

            var sortedPortfolio = portfolio.GroupBy(a => a.Name).Select(group => new PortfolioItem
            {
                Name = group.Key,
                AmountHistory = string.Join(",",group.Select(a=>a.Amount)),
                AmountInvested = group.Max(a=>a.AmountInvested),
                Type = group.Max(a=>a.Type),
                AmountRecordedDateHistory = string.Join(",", group.Select(a => a.Date)),
                LatestImportedAmountDate = group.Max(a=>a.Date).ToString("yyyy-MM-dd"),
                LatestImportedAmount = group.First(a=>a.Date == group.Max(group1 => 
                    group1.Date)).Amount,

            });

            sortedPortfolio = sortedPortfolio.OrderByDescending(a => a.AmountInvested);
                

            return sortedPortfolio;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
