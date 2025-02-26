using Microsoft.Extensions.DependencyInjection;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminPerformerProfilAlanlariLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSeslendirmeDiliLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSesRengiLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminVideoTagLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminVideoTipiLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.Egitim;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.FizikselOzellikler;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.FizikselOzellikler.Interfaces;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.KullaniciBasicLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.MenajerPerformerLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.OnerilerLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerAbonelikLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerAbonelikUrunuLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerCVLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerEtiketleriLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerFiltre;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerMenajerLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerPuanLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerTakvimler;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerYorumLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.SetCard;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.YetenekLogic;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.YetenekTemsilcisiLogicServices;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices;

public static class LogicServiceRegistration
{
    public static void AddlogicServices(this IServiceCollection services)
    {

        #region Admin

        services.AddScoped<IAdminEgitimLogicService, AdminEgitimLogicService>();
        services.AddScoped<IAdminYetenekLogicService, AdminYetenekLogicService>();
        services.AddScoped<IAdminFizikselOzellikLogicService, AdminFizikselOzellikLogicService>();
        services.AddScoped<IAdminPerformerProfilAlanlariLogicService, AdminPerformerProfilAlanlariLogicService>();

        #endregion

        services.AddScoped<IPerformerCVLogicService, PerformerCVLogicService>();
        services.AddScoped<IFotoAlbumLogicService, FotoAlbumLogicService>();
        services.AddScoped<IVideoAlbumLogicService, VideoAlbumLogicService>();
        services.AddScoped<IProfilOnayLogicService, ProfilOnayLogicService>();
        services.AddScoped<IProfilLogicService, ProfilLogicService>();
        services.AddScoped<IPerformerFiltreLogicService, PerformerFiltreLogicService>();
        services.AddScoped<IPerformerTakvimLogicService, PerformerTakvimLogicService>();
        services.AddScoped<ISetCardLogicService, SetCardLogicService>();
        services.AddScoped<IOnerilerLogicService, OnerilerLogicService>();
        services.AddScoped<IYetenekTemsilcisiLogicService, YetenekTemsilcisiLogicService>();
        services.AddScoped<IKullaniciBasicLogicService, KullaniciBasicLogicService>();
        services.AddScoped<IPerformerAbonelikUrunuLogicService, PerformerAbonelikUrunuLogicService>();
        services.AddScoped<IPerformerAbonelikLogicService, PerformerAbonelikLogicService>();
        services.AddScoped<IAdminSesRengiLogicService, AdminSesRengiLogicService>();
        services.AddScoped<IAdminSeslendirmeDiliLogicService, AdminSeslendirmeDiliLogicService>();
        services.AddScoped<IAdminVideoTipiLogicService, AdminVideoTipiLogicService>();
        services.AddScoped<IAdminVideoTagLogicService, AdminVideoTagLogicService>();
        services.AddScoped<IPerformerMenajerLogicService, PerformerMenajerLogicService>();
        services.AddScoped<IPerformerPuanLogicService, PerformerPuanLogicService>();
        services.AddScoped<IMenajerPerformerLogicService, MenajerPerformerLogicService>();
        services.AddScoped<IPerformerEtiketleriLogicService, PerformerEtiketleriLogicService>();
        services.AddScoped<IPerformerYorumLogicService, PerformerYorumLogicService>();
    }
}