using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CPL.Core.Services
{

    public class ErrorMessageService : ApiController, IErrorMessageService
    {
        public IHttpActionResult BadRequestMessage(HttpRequestMessage Request, string message)
        {
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
        }

        public IHttpActionResult Error404Message(HttpRequestMessage Request, string message)
        {
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
        }

    }
}
