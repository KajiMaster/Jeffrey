using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RallyNow.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RallyNow.API.Controllers
{
    [Route("api/[controller]")]
    public class ActivitiesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Activity> Get()
        {
            return new List<Activity>
            {
                new Activity
                {
                    Id = 1,
                    ActivityType = "Opportunity",
                    Action = "Posted a Message",
                    Message = "This is a test message",
                    Date = DateTime.Now
                },
                new Activity
                {
                    Id = 2,
                    ActivityType = "Opportunity",
                    Action = "Posted a Message",
                    Message = "This is a test message",
                    Date = DateTime.Now
                },
                new Activity
                {
                    Id = 3,
                    ActivityType = "Opportunity",
                    Action = "Posted a Message",
                    Message = "This is a test message",
                    Date = DateTime.Now
                }
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
