using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace Webzine.WebApplication.Filters
{
    public class LoggerActionFilter : IExceptionFilter
    {
        private static readonly NLog.ILogger Logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var exception = context.Exception;
                var controllerName = context.RouteData.Values["controller"] ?? "Inconnu";
                var actionName = context.RouteData.Values["action"] ?? "Inconnu";
                var message = $"EXCEPTION NON GÉRÉE: {exception.Message} dans {controllerName}/{actionName}";
                var stackTrace = $"Stack Trace: {exception.StackTrace}";
                Logger.Error(exception, message);
                Logger.Error(stackTrace);
                context.Result = new ViewResult 
                { 
                    ViewName = "Error",
                    ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
                    {
                        ["ErrorMessage"] = exception.Message,
                        ["ControllerName"] = controllerName,
                        ["ActionName"] = actionName,
                        ["StackTrace"] = exception.StackTrace
                    }
                };  
                context.HttpContext.Response.StatusCode = 500;
                context.ExceptionHandled = true;
            }
        }
    }
}