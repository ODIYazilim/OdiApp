using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace OdiApp.BusinessLayer.Core.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            var route = context.HttpContext.Request.Path.Value;
            var httpMethod = context.HttpContext.Request.Method;
            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) userId = "unauthenticated";
            _logger.LogError(
                context.Exception,
                "Bir hata oluştu; Route: " + route + "; Method: " + httpMethod + "; User ID: " + userId + "; DateTime:" + DateTime.Now + "; Error: " + context.Exception.Message,
                route,
                httpMethod,
                userId ?? "Not authenticated",
                DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                context.Exception.Message
            );

            context.Result = new ObjectResult(new
            {
                Success = false,
                Message = "İşlem sırasında bir hata oluştu",
                Error = context.Exception.Message,
                Route = route,
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            })
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
            base.OnException(context);
        }
    }
}
