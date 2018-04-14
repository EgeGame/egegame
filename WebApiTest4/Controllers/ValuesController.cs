using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTest4.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route("api/v1/Values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        [Route("api/v1/Values/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("api/v1/Values")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        [Route("api/v1/Values/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/v1/Values/{id}")]
        public void Delete(int id)
        {
        }
    }
}
