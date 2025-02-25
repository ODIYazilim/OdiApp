
using Microsoft.Extensions.DependencyInjection;
using OdiApp.BusinessLayer.Services.Kullanici;
using OdiApp.BusinessLayer.Services.Token;

namespace OdiApp.BusinessLayer
{
    public static class BussinessServisRegistration
    {

        public static void AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IKullaniciLogicService, KullaniciLogicService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

        }
    }
}
