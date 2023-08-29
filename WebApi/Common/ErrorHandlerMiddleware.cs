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
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case InvalidTokenException e:
                        response.StatusCode= (int)HttpStatusCode.Unauthorized;
                        break;
                    case UpdateUserException e:
                        response.StatusCode=(int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException  e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case EnumExeption e:
                        response.StatusCode=(int)(HttpStatusCode)HttpStatusCode.BadRequest;
                        break;
                    case ForbidenException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);

            }
        }
    }
}
