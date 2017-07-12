using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NLog;

namespace OnlineWallet.UI.Framework.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public CustomExceptionFilter( IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public override void OnException(ExceptionContext context)
        {
            Logger.Error(context.Exception);
            if (context.ExceptionHandled || context.HttpContext.Request.Path.StartsWithSegments("/api"))
            {
                return;
            }
            context.ModelState.AddModelError(context.Exception.HResult.ToString(), context.Exception.Message);
            var viewname = context.RouteData.Values["action"].ToString();
            var result = new ViewResult
            {
                ViewName = viewname,
                ViewData =
                    new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
                    {
                        {"Exception", context.Exception}
                    }
            };
            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}