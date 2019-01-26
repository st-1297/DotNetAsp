using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASPDotNetWebApi01.Controllers
{
    public class HelloWorldController : ApiController
    {
        // /api/HelloWorld
        public string Get()
        {
            return "Hello World!!";
        }
    }
}
