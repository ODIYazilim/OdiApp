using Microsoft.AspNetCore.Http;
using OdiApp.DTOs.GlobalDTOs;
using System.Globalization;
using System.Text.Json;

namespace OdiApp.BusinessLayer.Core.Exceptions
{
    public class LocalizationHeaderException
    {
        private readonly RequestDelegate _next;
        public LocalizationHeaderException(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            if (!context.Request.Headers.Keys.Contains("Accept-Language"))
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync(JsonSerializer.Serialize(OdiResponse<NoData>.Fail("Header, 'Accept-Language' değeri içermelidir.", "", 400)));
                throw new BadRequestException("Header, 'Accept-Language' değeri içermelidir.");
            }
            else
            {
                string lng = context.Request.Headers["Accept-Language"];
                if (string.IsNullOrEmpty(lng))
                {
                    throw new BadRequestException("'Accept-Language' değeri boş olamaz.");
                }
                switch (lng)
                {
                    case "odiDil-en":
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                        break;
                    case "odiDil-tr":
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("tr");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr");
                        break;
                    default:
                        throw new BadRequestException("Geçersiz 'Accept-Language' değeri.");
                }

                await _next.Invoke(context);
            }
        }
    }
}
