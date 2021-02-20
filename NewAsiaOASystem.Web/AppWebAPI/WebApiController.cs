using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetSystem.Web.WebAPI
{
    public class WebApiController : ApiController
    {
       
        public string GetProductsByCategory()
        {
            return "aa";
        }

        public string GetProductsByCategory1(int id=1)
        {
            return "aa";
        }

    }
}
