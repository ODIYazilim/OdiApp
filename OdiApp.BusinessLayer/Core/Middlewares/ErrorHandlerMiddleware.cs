using Microsoft.AspNetCore.Http;
using OdiApp.BusinessLayer.Core.Exceptions;
using OdiApp.DTOs.SharedDTOs;
using System.Net;
using System.Text.Json;

namespace OdiApp.BusinessLayer.Core.Middlewares
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

#if DEBUG
                Console.WriteLine(error.ToString());
#endif

                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case BadRequestException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundExcepiton e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case UnAuthorizeException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case DataNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                context.Response.StatusCode = response.StatusCode;


                await context.Response.WriteAsync(JsonSerializer.Serialize(OdiResponse<DTOs.SharedDTOs.NoContent>.Fail("Bir Sorun Oluştu", error?.Message, response.StatusCode)));

            }
        }
    }
}
