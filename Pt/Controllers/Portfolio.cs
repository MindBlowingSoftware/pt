using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PtShared;

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
            var combinedView = _dbContext.Query<CombinedView>();
            IQueryable<PortfolioItem> portfolio = GetPortfolioGroupedByCode(combinedView);
            portfolio = portfolio.OrderBy(a => a.Type).OrderByDescending(a => a.AmountInvested);
            return portfolio;
        }

        

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public PortfolioItem Get(string id)
        {
            var combinedView = _dbContext.Query<CombinedView>().Where(a=>a.Code == id);

            var portfolio = new PortfolioItem()
            {
                Id = combinedView.Max(b => b.Id),
                AmountInvested = Math.Round(combinedView.Max(b => b.AmountInvested) / 50, 2),
                Amount = Math.Round(combinedView.OrderByDescending(b => b.ValueDate).First().Value / 50, 2),
                Date = combinedView.OrderByDescending(b => b.ValueDate).First().ValueDate,
                Name = combinedView.Max(b => b.Name),
                Type = combinedView.Max(b => b.Type),
                AmountHistory = combinedView.OrderByDescending(b => b.ValueDate)
                        .Take(20)
                        .OrderBy(a => a.ValueDate)
                        .Select(b => Math.Round(b.Value / 50, 2)).ToArray(),
                AmountRecordedDateHistory = combinedView.OrderByDescending(b => b.ValueDate)
                        .Take(20)
                        .OrderBy(a => a.ValueDate)
                        .Select(b => b.ValueDate).ToArray(),
                Code = id
            };
            return portfolio;
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

        private static IQueryable<PortfolioItem> GetPortfolioGroupedByCode(IQueryable<CombinedView> combinedView)
        {
            return combinedView
                .GroupBy(a => a.Code)
                .Select(group => new PortfolioItem
                {
                    Id = group.Max(b => b.Id),
                    AmountInvested = Math.Round(group.Max(b => b.AmountInvested) / 50,2),
                    Amount = Math.Round(group.OrderByDescending(b => b.ValueDate).First().Value / 50, 2),
                    Date = group.OrderByDescending(b => b.ValueDate).First().ValueDate,
                    Name = group.Max(b => b.Name),
                    Type = group.Max(b => b.Type),
                    AmountHistory = group.OrderByDescending(b => b.ValueDate)
                        .Take(20)
                        .OrderBy(a=>a.ValueDate)
                        .Select(b => Math.Round(b.Value / 50, 2)).ToArray(),
                    AmountRecordedDateHistory = group.OrderByDescending(b => b.ValueDate)
                        .Take(20)
                        .OrderBy(a => a.ValueDate)
                        .Select(b => b.ValueDate).ToArray(),
                    Code = group.Key
                });
        }
    }
}
