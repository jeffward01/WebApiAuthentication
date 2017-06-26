using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CPL.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("TestMethod")]
        public IHttpActionResult TestMethod()
        {
            return Ok("This is working");
        }
    }
}