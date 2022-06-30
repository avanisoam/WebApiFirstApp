using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiFirstApp.Controllers
{
    public class ValuesController : ApiController
    {
        //    GET api/values
        //    public IEnumerable<string> Get()
        //    {
        //        return new string[] { "value1", "value2" };
        //    }

        //    // GET api/values/5
        //    public string Get(int id)
        //    {
        //        return "value";
        //    }

        //    // POST api/values
        //    public void Post([FromBody]string value)
        //    {
        //    }

        //    // PUT api/values/5
        //    public void Put(int id, [FromBody]string value)
        //    {
        //    }

        //    // DELETE api/values/5
        //    public void Delete(int id)
        //    {
        //    }

        static List<string> strings = new List<string>()
        {
            "value0", "value1", "value2"
        };

        public IEnumerable<string> Get()
        {
            return new string[] {"value11" , "value22s" }; ;
        }

        public string Get(int id)
        {
            return strings[id];
        }

        public void Post([FromBody]string value)
        {
            strings.Add(value);
        }

        public void Put(int id, [FromBody]string value)
        {
            strings[id] = value;
        }

        public void Delete(int id)
        {
            strings.RemoveAt(id);
        }
    }
}
