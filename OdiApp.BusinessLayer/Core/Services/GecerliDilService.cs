using Microsoft.AspNetCore.Http;
using OdiApp.BusinessLayer.Core.Exceptions;
using OdiApp.BusinessLayer.Core.Services.Interface;

namespace OdiApp.BusinessLayer.Core.Services
{
    public class GecerliDilService : IGecerliDilService
    {
        IHttpContextAccessor _httpContext;
        public GecerliDilService(IHttpContextAccessor context)
        {
            _httpContext = context;
        }

        public async Task<int> GecerliDil()
        {

            string dil = _httpContext.HttpContext.Request.Headers["Accept-Language"];
            int dilId = 0;
            switch (dil)
            {
                case "odiDil-en":
                    //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    dilId = 2;
                    break;
                case "odiDil-tr":
                    //Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
                    //Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR");
                    dilId = 1;
                    break;
                default:

                    throw new BadRequestException("Geçersiz 'Accept-Language' değeri.");
                    break;
            }

            return dilId;
        }
    }
}