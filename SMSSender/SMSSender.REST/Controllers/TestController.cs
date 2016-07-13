using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SMSSender.REST.Controllers
{
    public class TestController : ApiController
    {
        [AcceptVerbs("GET")]
        public string Welcome()
        {
            return $"SMS Sender v{typeof(TestController).Assembly.GetName().Version}";
        }

        [AcceptVerbs("GET")]
        public bool TestBool()
        {
            return true;
        }

        [AcceptVerbs("GET")]
        public string TestString()
        {
            return "Hello World!";
        }
    }
}
