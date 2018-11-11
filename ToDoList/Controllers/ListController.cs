using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ToDoList.Controllers
{
    public class ListController : ApiController
    {
        // GET: api/List
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/List/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/List
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/List/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/List/5
        public void Delete(int id)
        {
        }
    }
}
