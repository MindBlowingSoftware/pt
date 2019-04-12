using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PtShared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pt.Controllers
{
    [Route("api/[controller]")]
    public class SuperController : Controller
    {
        private PTContext _dbContext;
        private static readonly decimal exr = 0.75m;
        public SuperController(PTContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<controller>
        [HttpGet("[action]")]
        public IEnumerable<PortfolioItem> Get()
        {
            var combinedView = _dbContext.Query<CombinedView>().Where(a => a.Type == "SMSF").ToList();
            List<PortfolioItem> portfolio = Shared.GetPortfolioGroupedByCode(combinedView, exr);
            portfolio = portfolio.OrderBy(a => a.Type).ThenByDescending(a => a.AmountInvested).ToList();
            return portfolio;
        }

        

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public PortfolioItem Get(string id)
        {
            var combinedView = _dbContext.Query<CombinedView>().Where(a=>a.Code == id);

            return Shared.Get(combinedView, exr);
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
