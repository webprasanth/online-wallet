using System;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using jQWidgets.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace OnlineWallet.UI.Framework
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleAsync(context,ex);
            }
        }

        private static async Task HandleAsync(HttpContext context,Exception exception)
        {
            var errorCode = "Error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();

            switch (exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case Exception e when exceptionType == typeof(InvalidOperationException):
                    statusCode = HttpStatusCode.TemporaryRedirect;
                    break;

                case Exception e when exceptionType == typeof(ArgumentNullException):
                    statusCode = HttpStatusCode.Forbidden;
                    break;

                //TO DO: more Exceptions

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)statusCode;
            var response = $"{errorCode} {context.Response.StatusCode}\n{exception.Message}\n";
            await context.Response.WriteAsync(response);
        }
    }
}