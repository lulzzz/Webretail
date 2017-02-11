using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Webretail.Admin.Attributes
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var loggerFactory =
                (ILoggerFactory)context.HttpContext.RequestServices.GetService(typeof(ILoggerFactory));

            var logger = loggerFactory.CreateLogger("Webretail.WebretailService");
            logger.LogError(2, context.Exception.Message, context.Exception);

            context.Result = new BadRequestObjectResult(context.Exception.Message);
        }
    }
}
