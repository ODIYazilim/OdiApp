using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdiApp.DTOs.TokenDTOs;
using System.Reflection;

namespace OdiApp.BusinessLayer.Core
{
    public static class CoreServicesRegistration
    {
        public static void AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));// appsettings teki token optionları almak için

            services.Configure<List<Client>>(configuration.GetSection("Clients")); // appsetting s teki clientsları almak için
        }
    }
}
