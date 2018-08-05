using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PtShared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pt.Controllers
{
    [Route("api/[controller]")]
    public class SavingsController : Controller
    {
        private PTContext _dbContext;
        public SavingsController(PTContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<controller>
        [HttpGet("[action]")]
        public IEnumerable<Savings> Get()
        {
            return _dbContext.Savings;
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
