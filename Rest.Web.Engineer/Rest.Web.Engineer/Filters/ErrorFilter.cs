using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Rest.Web.Engineer.Filters
{
    public class ErrorFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError,
                actionExecutedContext.Exception);
        }
    }
}
