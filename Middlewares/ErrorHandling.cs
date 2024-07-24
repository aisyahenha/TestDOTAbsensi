using System.Net;
using TestAbsensi.Exeptions;
using TestAbsensi.Models;

namespace TestAbsensi.Middlewares
{
    public class ErrorHandling : IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandling(ILogger<ErrorHandling> logger)
        {
            _logger = logger;
        }



        private static async Task HandlingException(HttpContext context, Exception e)
        {
            var error = new ErrorResponse();

            switch (e)
            {
                case BadRequest:
                    error.Code = (int)HttpStatusCode.BadRequest;
                    error.Status = HttpStatusCode.BadRequest.ToString();
                    error.Message = e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFound:
                    error.Code = (int)HttpStatusCode.NotFound;
                    error.Status = HttpStatusCode.NotFound.ToString();
                    error.Message = e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
            }

            await context.Response.WriteAsJsonAsync(error);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandlingException(context, e);
                _logger.LogError(e.Message);
            }
        }

    }
}
