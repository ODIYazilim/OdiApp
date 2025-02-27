using Microsoft.Extensions.DependencyInjection;
using OdiApp.DataAccessLayer.BildirimDataServices.MesajlasmaDataServices;
using OdiApp.DataAccessLayer.BildirimDataServices.MutluCellSmsDataServices;
using OdiApp.DataAccessLayer.BildirimDataServices.OdiBildirimDataServices;
using OdiApp.DataAccessLayer.BildirimDataServices.OneSignalDataServices;
using OdiApp.DataAccessLayer.BildirimDataServices.ProjeMesajlasmaDataServices;
using OdiApp.DataAccessLayer.IslemlerDataServices.CallbackIslemler;
using OdiApp.DataAccessLayer.IslemlerDataServices.OdiIslemler;
using OdiApp.DataAccessLayer.IslemlerDataServices.OdiListeler;
using OdiApp.DataAccessLayer.IslemlerDataServices.OpsiyonIslemler;
using OdiApp.DataAccessLayer.IslemlerDataServices.PerformerListeler;
using OdiApp.DataAccessLayer.IslemlerDataServices.RolSendeDataServices;
using OdiApp.DataAccessLayer.Kullanici;
using OdiApp.DataAccessLayer.PerformerDataServices.CVFormAlanlariDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.DeneyimDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.Egitim;
using OdiApp.DataAccessLayer.PerformerDataServices.FizikselOzellikler;
using OdiApp.DataAccessLayer.PerformerDataServices.FotografAlbum;
using OdiApp.DataAccessLayer.PerformerDataServices.KisiselOzelliklerDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.KullaniciBasicDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.MenajerPerformerGuncellenenAlaniDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.MenajerPerformerNotDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.OnerilerDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerAbonelikUrunuDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerCVs.Interfaces;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerEtiketleriDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerFiltre;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerMenajerSozlesmeDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerProfilAlanlariDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerPuanDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerTakvimler;
using OdiApp.DataAccessLayer.PerformerDataServices.PerformerYorumDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.ProfilOnayDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.SektorDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.SeslendirmeDiliDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.SesRengiDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.SetCard;
using OdiApp.DataAccessLayer.PerformerDataServices.VideoAlbumm;
using OdiApp.DataAccessLayer.PerformerDataServices.VideoTagDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.VideoTipiDataServices;
using OdiApp.DataAccessLayer.PerformerDataServices.YetenekData;
using OdiApp.DataAccessLayer.PerformerDataServices.YetenekTemsilcisiDataServices;
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

            #region Performer

            services.AddScoped<ISektorDataService, SektorDataService>();

            //
            services.AddScoped<IPerformerMenajerSozlesmeDataService, PerformerMenajerSozlesmeDataService>();

            //
            services.AddScoped<IFizikselOzellikDataService, FizikselOzellikDataService>();
            services.AddScoped<IKisiselOzelliklerDataService, KisiselOzelliklerDataService>();


            services.AddScoped<IEgitimDataService, EgitimDataService>();

            services.AddScoped<IYetenekDataService, YetenekDataService>();
            services.AddScoped<IDeneyimDataService, DeneyimDataService>();


            //

            services.AddScoped<ICVEgitimDataService, CVEgitimDataService>();

            services.AddScoped<ICVYetenekDataService, CVYetenekDataService>();

            services.AddScoped<ICVDataService, CVDataService>();
            services.AddScoped<ICVFormAlanlariDataService, CVFormAlanlariDataService>();


            services.AddScoped<IPerformerCVDataService, PerformerCVDataService>();

            //

            services.AddScoped<IFotoAlbumTipiDataService, FotoAlbumTipiDataService>();
            services.AddScoped<IFotoAlbumDataService, FotoAlbumDataService>();

            services.AddScoped<IVideoAlbumTipiDataService, VideoAlbumTipiDataService>();
            services.AddScoped<IVideoAlbumDataService, VideoAlbumDataService>();

            //

            services.AddScoped<IProfilOnayDataService, ProfilOnayDataService>();

            //serv
            services.AddScoped<IPerformerFiltreDataService, PerformerFiltreDataService>();

            services.AddScoped<IPerformerTakvimDataService, PerformerTakvimDataService>();
            services.AddScoped<ISetCardDataService, SetCardDataService>();


            // Oneriler
            services.AddScoped<IOnerilerDataService, OnerilerDataService>();

            // YetenekTemsilcisi
            services.AddScoped<IYetenekTemsilcisiDataService, YetenekTemsilcisiDataService>();

            services.AddScoped<IKullaniciBasicDataService, KullaniciBasicDataService>();

            services.AddScoped<IPerformerAbonelikUrunuDataService, PerformerAbonelikUrunuDataService>();

            services.AddScoped<IPerformerAbonelikDataService, PerformerAbonelikDataService>();

            services.AddScoped<IPerformerProfilAlanlariDataService, PerformerProfilAlanlariDataService>();

            services.AddScoped<ISesRengiDataService, SesRengiDataService>();

            services.AddScoped<ISeslendirmeDiliDataService, SeslendirmeDiliDataService>();

            services.AddScoped<IVideoTipiDataService, VideoTipiDataService>();

            services.AddScoped<IVideoTagDataService, VideoTagDataService>();

            services.AddScoped<IPerformerPuanDataService, PerformerPuanDataService>();

            services.AddScoped<IMenajerPerformerGuncellenenAlaniDataService, MenajerPerformerGuncellenenAlaniDataService>();

            services.AddScoped<IMenajerPerformerNotDataService, MenajerPerformerNotDataService>();

            services.AddScoped<IPerformerEtiketleriDataService, PerformerEtiketleriDataService>();

            services.AddScoped<IPerformerYorumDataService, PerformerYorumDataService>();

            #endregion

            #region Bildirim

            services.AddScoped<IOdiBildirimDataService, OdiBildirimDataService>();
            services.AddScoped<IOneSignalUserDataService, OneSignalUserDataService>();
            services.AddScoped<IMesajlasmaDataService, MesajlasmaDataService>();
            services.AddScoped<IProjeMesajlasmaDataService, ProjeMesajlasmaDataService>();
            services.AddScoped<IKullaniciBasicDataService, KullaniciBasicDataService>();
            services.AddScoped<IMutluCellSmsDataService, MutluCellSmsDataService>();

            #endregion

            #region Islemler

            services.AddScoped<IOdiIslemDataService, OdiIslemDataService>();
            services.AddScoped<IPerformerListeDataService, PerformerListeDataService>();
            services.AddScoped<IPerformerOdiDataService, PerformerOdiDataService>();
            services.AddScoped<ICallbackDataService, CallbackDataService>();
            services.AddScoped<IOpsiyonDataService, OpsiyonDataService>();
            services.AddScoped<IOdiListeDataService, OdiListeDataService>();
            services.AddScoped<IKullaniciBasicDataService, KullaniciBasicDataService>();
            services.AddScoped<IRolSendeDataService, RolSendeDataService>();

            #endregion
        }
    }
}