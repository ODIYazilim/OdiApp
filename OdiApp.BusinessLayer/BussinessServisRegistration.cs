
using Microsoft.Extensions.DependencyInjection;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.KullaniciBasicLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.MailLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.MesajlasmaLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.MutluCellSmsLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.OdiBildirimLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.OneSignalLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.ProjeMesajlasmaLogicServices;
using OdiApp.BusinessLayer.Services.BildirimLogicServices.PushNotificationServices;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.CallbackIslemler;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiIslemler;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiListeler;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.OpsiyonIslemler;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.PerformerIslemler;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.PerformerListeler;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.ProjePerformer;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.RolSendeLogicServices;
using OdiApp.BusinessLayer.Services.Kullanici;
using OdiApp.BusinessLayer.Services.OdemeLogicServices.AbonelikUrunuLogicServices;
using OdiApp.BusinessLayer.Services.OdemeLogicServices.IyzicoLogicServices;
using OdiApp.BusinessLayer.Services.OdemeLogicServices.OdicikIslemleriLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminPerformerProfilAlanlariLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSeslendirmeDiliLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminSesRengiLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminVideoTagLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.AdminVideoTipiLogicServices;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.Egitim;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.FizikselOzellikler;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.FizikselOzellikler.Interfaces;
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
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.KayitliRolLogicServices;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeBilgileri;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolBilgileri;
using OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolOdiBilgileri;
using OdiApp.BusinessLayer.Services.ShareWithOtherServicess;
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

            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            services.AddScoped<IGecerliDilService, GecerliDilService>();
            services.AddScoped<IAmazonS3Service, AmazonS3Service>();
            services.AddScoped<IUseOtherService, UseOtherService>();

            #region Admin

            services.AddScoped<IAdminEgitimLogicService, AdminEgitimLogicService>();
            services.AddScoped<IAdminYetenekLogicService, AdminYetenekLogicService>();
            services.AddScoped<IAdminFizikselOzellikLogicService, AdminFizikselOzellikLogicService>();
            services.AddScoped<IAdminPerformerProfilAlanlariLogicService, AdminPerformerProfilAlanlariLogicService>();

            #endregion

            #region Bildirim

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

            #endregion

            #region Performer

            services.AddScoped<IOdiBildirimLogicService, OdiBildirimLogicService>();
            services.AddScoped<IOneSignalLogicService, OneSignalLogicService>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            services.AddScoped<IMesajLogicService, MesajLogicService>();
            services.AddScoped<IProjeMesajLogicService, ProjeMesajLogicService>();
            services.AddScoped<IKullaniciBasicLogicService, KullaniciBasicLogicService>();
            services.AddScoped<IMutluCellSmsLogicService, MutluCellSmsLogicService>();
            services.AddScoped<ITicketMailLogicService, TicketMailLogicService>();

            #endregion

            #region Islemler

            services.AddScoped<IProjePerformerLogicService, ProjePerformerLogicService>();
            services.AddScoped<IOdiIslemLogicService, OdiIslemLogicService>();
            services.AddScoped<IPerformerListeLogicService, PerformerListeLogicService>();
            services.AddScoped<IPerforlerIslemlerLogicService, PerformerIslemlerLogicService>();
            services.AddScoped<IPerformerOdiLogicService, PerformerOdiLogicService>();
            services.AddScoped<IOdiListeLogicService, OdiListeLogicService>();
            services.AddScoped<ICallbackLogicService, CallbackLogicService>();
            services.AddScoped<IOpsiyonLogicService, OpsiyonLogicService>();
            services.AddScoped<IKullaniciBasicLogicService, KullaniciBasicLogicService>();
            services.AddScoped<IRolSendeLogicService, RolSendeLogicService>();

            #endregion

            #region Projeler

            services.AddScoped<IProjeLogicService, ProjeLogicService>();
            services.AddScoped<IProjeRolLogicService, ProjeRolLogicService>();
            services.AddScoped<IProjeRolOdiLogicService, ProjeRolOdiLogicService>();
            services.AddScoped<IShareWithOtherServices, ShareWithOtherServices>();
            services.AddScoped<IKullaniciBasicLogicService, KullaniciBasicLogicService>();
            services.AddScoped<IKayitliRolLogicService, KayitliRolLogicService>();

            #endregion

            #region Odeme

            services.AddScoped<IOdicikIslemleriLogicService, OdicikIslemleriLogicService>();
            services.AddScoped<IIyzicoLogicService, IyzicoLogicService>();
            services.AddScoped<IAbonelikUrunuLogicService, AbonelikUrunuLogicService>();
            services.AddScoped<IKullaniciBasicLogicService, KullaniciBasicLogicService>();

            #endregion
        }
    }
}