using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineWallet.Infrastructure.Services;

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
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();

            switch (exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case ServiceException e when exceptionType == typeof(ServiceException):
                    errorCode = e.Code;
                    break;

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