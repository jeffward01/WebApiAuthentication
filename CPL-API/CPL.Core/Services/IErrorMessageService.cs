using System.Net.Http;
using System.Web.Http;

namespace CPL.Core.Services
{
    public interface IErrorMessageService
    {
        IHttpActionResult BadRequestMessage(HttpRequestMessage Request, string message);
        IHttpActionResult Error404Message(HttpRequestMessage Request, string message);
    }
}