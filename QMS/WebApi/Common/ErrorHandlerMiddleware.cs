using Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace WebApi.Common
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                if (context.Response.HasStarted)
                {
                    // Nuk mund të ndryshosh headers pasi përgjigjja ka nisur
                    Console.WriteLine("[ErrorHandler] Response has already started. Cannot write error.");
                    Console.WriteLine(error); // Ose log në ndonjë logger
                    return;
                }

                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    AppException => (int)HttpStatusCode.InternalServerError,
                    InvalidTokenException => (int)HttpStatusCode.Unauthorized,
                    EnumExeption => (int)HttpStatusCode.BadRequest,
                    ForbidenException => (int)HttpStatusCode.Forbidden,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);


            }
        }
    }
}
