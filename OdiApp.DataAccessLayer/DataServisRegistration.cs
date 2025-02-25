using Microsoft.Extensions.DependencyInjection;
using OdiApp.DataAccessLayer.Kullanici;
using OdiApp.DataAccessLayer.Token;
namespace OdiApp.DataAccessLayer
{
    public static class DataServisRegistration
    {
        public static void AddDataLayerServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IKullaniciDataService, KullaniciDataService>();
            services.AddScoped<IUserRefreshTokenDataService, UserRefreshTokenDataService>();


        }
    }
}
