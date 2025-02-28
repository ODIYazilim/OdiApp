using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.EntityLayer.Base;
using OdiApp.EntityLayer.BildirimModels;
using OdiApp.EntityLayer.BildirimModels.Mesajlasma;
using OdiApp.EntityLayer.BildirimModels.ProjeMesajlasma;
using OdiApp.EntityLayer.BildirimModels.SmsAyarlariModels;
using OdiApp.EntityLayer.Identity;
using OdiApp.EntityLayer.IslemlerModels.CallbackIslemler;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;
using OdiApp.EntityLayer.IslemlerModels.OdiListeler;
using OdiApp.EntityLayer.IslemlerModels.OpsiyonIslemler;
using OdiApp.EntityLayer.IslemlerModels.PerformerListeler;
using OdiApp.EntityLayer.IslemlerModels.RolSendeModels;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;
using OdiApp.EntityLayer.OdemeModels.OdicikModels;
using OdiApp.EntityLayer.PerformerModels.Deneyimler;
using OdiApp.EntityLayer.PerformerModels.Egitim;
using OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;
using OdiApp.EntityLayer.PerformerModels.FotografAlbum;
using OdiApp.EntityLayer.PerformerModels.KisiselOzellikler;
using OdiApp.EntityLayer.PerformerModels.MenajerPerformerNotModels;
using OdiApp.EntityLayer.PerformerModels.OnerilerModels;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikModels;
using OdiApp.EntityLayer.PerformerModels.PerformerAbonelikUrunModels;
using OdiApp.EntityLayer.PerformerModels.PerformerCVModels;
using OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;
using OdiApp.EntityLayer.PerformerModels.PerformerMenajerModels;
using OdiApp.EntityLayer.PerformerModels.PerformerProfilModels;
using OdiApp.EntityLayer.PerformerModels.PerformerPuanModels;
using OdiApp.EntityLayer.PerformerModels.PerformerTakvimModels;
using OdiApp.EntityLayer.PerformerModels.PerformerYorumModels;
using OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;
using OdiApp.EntityLayer.PerformerModels.SektorModels;
using OdiApp.EntityLayer.PerformerModels.SeslendirmeDiliModels;
using OdiApp.EntityLayer.PerformerModels.SesRengiModels;
using OdiApp.EntityLayer.PerformerModels.VideoTipiModels;
using OdiApp.EntityLayer.PerformerModels.YetenekModels;
using OdiApp.EntityLayer.PerformerModels.YetenekTemsilcisiModels;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSes;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSoru;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiVideo;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolBilgisi;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolOdi;
using OdiApp.EntityLayer.ProjelerModels.OdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.OdiSes;
using OdiApp.EntityLayer.ProjelerModels.OdiSoru;
using OdiApp.EntityLayer.ProjelerModels.OdiVideo;
using OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolOdi;
using OdiApp.EntityLayer.SharedModels;
using OdiApp.EntityLayer.Token;
using OdiApp.EntityLayer.UygulamaBilgileriModels;
using System.Security.Claims;
using AbonelikUrunu = OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels.AbonelikUrunu;

namespace OdiApp.DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
           : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        #region Performer

        #region Sektörler

        public DbSet<Sektor> Sektorler { get; set; }

        #endregion

        #region Performer Yorum

        public DbSet<PerformerYorum> PerformerYorumlari { get; set; }

        #endregion

        #region Performer Etiket

        public DbSet<YetenekTemsilcisiPerformerEtiketTipi> YetenekTemsilcisiPerformerEtiketTipleri { get; set; }
        public DbSet<PerformerEtiket> PerformerEtiketleri { get; set; }
        public DbSet<YetenekTemsilcisiPerformerEtiketi> YetenekTemsilcisiPerformerEtiketleri { get; set; }

        #endregion

        #region Menajer Performer Not

        public DbSet<MenajerPerformerNot> MenajerPerformerNotlari { get; set; }

        #endregion

        #region Performer Puan

        public DbSet<PerformerPuan> PerformerPuanlari { get; set; }

        #endregion

        #region Performer Menajer

        public DbSet<PerformerMenajerSozlesme> PerformerMenajerSozlesmeleri { get; set; }

        #endregion

        #region Performer Abonelik

        public DbSet<PerformerAbonelikUrunu> PerformerAbonelikUrunleri { get; set; }
        public DbSet<PerformerAbonelik> PerformerAbonelikleri { get; set; }
        public DbSet<PerformerAbonelikSureUzatma> PerformerAbonelikSureUzatmalari { get; set; }

        #endregion

        #region Performer Profil

        public DbSet<PerformerProfilAlanlari> PerformerProfilAlanlari { get; set; }

        #endregion

        #region Ses Renkleri

        public DbSet<SesRengi> SesRenkleri { get; set; }

        #endregion

        #region Seslendirme Dilleri

        public DbSet<SeslendirmeDili> SeslendirmeDilleri { get; set; }

        #endregion

        #region FİZİKSEL ÖZELLİKLER



        public DbSet<FizikselOzellikTipi> FizikselOzellikTipleri { get; set; }
        public DbSet<FizikselOzellik> FizikselOzellikler { get; set; }

        #endregion

        #region Kişisel Ozellikler
        public DbSet<KisiselOzellik> KisiselOzellikler { get; set; }

        #endregion

        #region EĞİTİM

        public DbSet<OkulBolum> OkulBolumler { get; set; }
        public DbSet<Okul> Okullar { get; set; }
        public DbSet<EgitimTipi> EgitimTipleri { get; set; }

        #endregion

        #region YETENEK

        public DbSet<YetenekTipi> YetenekTipleri { get; set; }
        public DbSet<Yetenek> Yetenekler { get; set; }

        #endregion

        #region DENEYİM
        public DbSet<Deneyim> Deneyimler { get; set; }
        public DbSet<DeneyimFormAlani> DeneyimFormAlanlari { get; set; }
        public DbSet<DeneyimDetay> DeneyimDetaylari { get; set; }

        #endregion

        #region PERFORMERCV

        public DbSet<CVEgitim> CVEgitimler { get; set; }
        public DbSet<CVDeneyim> CVDeneyimler { get; set; }
        public DbSet<CVDeneyimDetay> CVDeneyimDetaylari { get; set; }

        public DbSet<CVYetenek> CVYetenekleri { get; set; }
        public DbSet<CVYetenekVideo> CVYetenekVideolari { get; set; }
        public DbSet<CVFizikselOzellik> CVFizikselOzellikler { get; set; }
        public DbSet<PerformerCV> PerformerCV { get; set; }
        public DbSet<CVFormAlani> CVFormAlanlari { get; set; }
        public DbSet<MenajerPerformerGuncellenenAlani> MenajerPerformerGuncellenenAlanlari { get; set; }
        public DbSet<CV> CVler { get; set; }
        public DbSet<CVData> CVDatalar { get; set; }

        #endregion

        #region FOTOĞRAF ALBÜM 
        public DbSet<FotoAlbumTipi> FotoAlbumTipleri { get; set; }
        public DbSet<FotoAlbumTipiLabel> FotoAlbumTipiLabels { get; set; }
        public DbSet<FotoAlbum> FotoAlbumleri { get; set; }
        public DbSet<FotoAlbumFotograf> FotoAlbumFotograflar { get; set; }

        #endregion

        #region Profil Videoları 
        public DbSet<VideoTipi> VideoTipleri { get; set; }
        public DbSet<ProfilVideo> ProfilVideolari { get; set; }
        //public DbSet<VideoTag> VideoTaglari { get; set; }
        //public DbSet<VideoAlbumTipi> VideoAlbumTipleri { get; set; }
        //public DbSet<VideoAlbumTipiLabel> VideoAlbumTipiLabels { get; set; }
        //public DbSet<VideoAlbum> VideoAlbumleri { get; set; }
        //public DbSet<VideoAlbumVideo> VideoAlbumVideolar { get; set; }

        #endregion

        #region Profil Onay

        public DbSet<ProfilOnay> ProfilOnaylari { get; set; }
        public DbSet<ProfilOnayRedNedeniTanimi> ProfilOnayRedNedeniTanimlari { get; set; }
        public DbSet<ProfilOnayRedNedeni> ProfilOnayRedNedenleri { get; set; }
        public DbSet<PerformerDondurulan> PerformerDondurulanlar { get; set; }
        public DbSet<PerformerEngellenen> PerformerEngellenenler { get; set; }

        #endregion

        #region Performer Takvim

        public DbSet<PerformerTakvim> PerformerTakvim { get; set; }

        #endregion

        #region Oneri

        public DbSet<OneriTalepleri> OneriTalepleri { get; set; }
        public DbSet<MenajerPerformerOnerileri> MenajerPerformerOnerileri { get; set; }

        #endregion

        #region PerformerYetenekTemsilcisi

        public DbSet<PerformerYetenekTemsilcisi> PerformerYetenekTemsilcisi { get; set; }

        #endregion

        #region Kullanıcı Basic

        public DbSet<KullaniciBasic> KullaniciBasic { get; set; }

        #endregion

        #endregion

        #region Bildirim        

        public DbSet<OneSignalUser> OneSignalUsers { get; set; }
        public DbSet<OneSignalUserSubscription> OneSignalUserSubscriptions { get; set; }
        public DbSet<OdiBildirim> OdiBildirim { get; set; }
        public DbSet<OdiBildirimHerkes> OdiBildirimHerkes { get; set; }
        public DbSet<Mesaj> Mesaj { get; set; }
        public DbSet<MesajDetay> MesajDetay { get; set; }
        public DbSet<ProjeMesaj> ProjeMesaj { get; set; }
        public DbSet<ProjeMesajDetay> ProjeMesajDetay { get; set; }
        public DbSet<MutluCellSmsAyarlari> MutluCellSmsAyarlari { get; set; }

        #endregion

        #region Islemler

        public DbSet<OdiTalep> OdiTalepleri { get; set; }
        public DbSet<PerformerOdi> PerformerOdi { get; set; }
        public DbSet<PerformerOdiSoru> PerformerOdiSorulari { get; set; }
        public DbSet<PerformerOdiFotograf> PerformerOdiFotograflar { get; set; }
        public DbSet<PerformerOdiSes> PerformerOdiSesler { get; set; }
        public DbSet<PerformerOdiVideo> PerformerOdiVideolar { get; set; }
        public DbSet<OdiListe> OdiListeleri { get; set; }
        public DbSet<OdiListeDetay> OdiListeDetay { get; set; }
        public DbSet<PerformerListe> PerformerListe { get; set; }
        public DbSet<PerformerListeDetay> PerformerListeDetay { get; set; }
        public DbSet<PerformerOdiTekrarCekOneri> PerformerOdiTekrarCekOnerileri { get; set; }
        public DbSet<OpsiyonListesi> OpsiyonListeleri { get; set; }
        public DbSet<Opsiyon> Opsiyon { get; set; }
        public DbSet<OpsiyonAnketSorulari> OpsiyonAnketSorulari { get; set; }

        public DbSet<CallbackAyarlari> CallbackAyarlari { get; set; }
        public DbSet<CallbackNot> CallbackNotlari { get; set; }
        public DbSet<CallbackSenaryo> CallbackSenaryolari { get; set; }
        public DbSet<CallbackSaat> CallbackSaatleri { get; set; }
        public DbSet<Callback> Callback { get; set; }
        public DbSet<RolSende> RolSende { get; set; }

        #endregion

        #region Odeme

        public DbSet<OdicikIslemleri> OdicikIslemleri { get; set; }
        public DbSet<AbonelikUrunu> AbonelikUrunleri { get; set; }
        public DbSet<AbonelikUrunuOdemePlani> AbonelikUrunuOdemePlanlari { get; set; }
        public DbSet<AbonelikPaketiSatinAlma> AbonelikPaketiSatinAlmalari { get; set; }
        public DbSet<AbonelikPaketiAboneOlma> AbonelikPaketiAboneOlmalari { get; set; }
        public DbSet<AbonelikKartlari> AbonelikKartlari { get; set; }
        public DbSet<AbonelikYukseltmeTalep> AbonelikYukseltmeTalepleri { get; set; }

        #endregion

        #region Projeler

        public DbSet<Proje> Projeler { get; set; }
        public DbSet<ProjeYetkili> ProjeYetkilileri { get; set; }
        public DbSet<ProjeTuru> ProjeTurleri { get; set; }
        public DbSet<ProjeDefaultLogo> ProjeDefaultLogolari { get; set; }
        public DbSet<ProjeKatilimBolgesi> ProjeKatilimBolgeleri { get; set; }

        //Proje ROL

        public DbSet<RolAgirlikTipi> RolAgirlikTipleri { get; set; }
        public DbSet<ProjeRol> ProjeRolleri { get; set; }
        public DbSet<ProjeRolAnketSorusu> ProjeRolAnketSorulari { get; set; }
        public DbSet<ProjeRolPerformer> ProjeRolPerformer { get; set; }
        public DbSet<ProjeRolOzellik> ProjeRolOzellikleri { get; set; }
        public DbSet<RolOzellikFiziksel> RolOzellikFizikseller { get; set; }
        public DbSet<RolOzellikDeneyim> RolOzellikDeneyimler { get; set; }
        public DbSet<RolOzellikEgitim> RolOzellikEgitimler { get; set; }
        public DbSet<RolOzellikYetenek> RolOzellikYetenekler { get; set; }
        public DbSet<RolOzellikPerformerEtiket> RolOzellikPerformerEtiketler { get; set; }

        //PROJE ROL ODİ

        public DbSet<ProjeRolOdi> ProjeRolOdileri { get; set; }

        //ROL ODİ FOTOMATiK

        public DbSet<RolOdiFotoPoz> RolOdiFotoPozlar { get; set; }
        public DbSet<RolOdiFotoOrnekFotograf> RolOdiFotoOrnekFotograflar { get; set; }
        public DbSet<RolOdiFotoYonetmenNotu> RolOdiFotoYonetmenNotlari { get; set; }

        //ROL ODİ SESMATİK

        public DbSet<RolOdiSes> RolOdiSesler { get; set; }
        public DbSet<RolOdiSesYonetmenNotu> RolOdiSesYonetmenNotlari { get; set; }
        public DbSet<RolOdiSesSenaryo> RolOdiSesSenaryolar { get; set; }

        //ROl Odi Videomatik
        public DbSet<RolOdiVideo> RolOdiVideolar { get; set; }
        public DbSet<RolOdiVideoDetay> RolOdiVideoDetaylar { get; set; }
        public DbSet<RolOdiVideoOrnekOyun> RolOdiVideoOrnekOyunlar { get; set; }
        public DbSet<RolOdiVideoSenaryo> RolOdiVideoSenaryolar { get; set; }
        public DbSet<RolOdiVideoYonetmenNotu> RolOdiVideoYonetmenNotlari { get; set; }

        //Rol Odi Sorumatik
        public DbSet<RolOdiSoru> RolOdiSorular { get; set; }
        public DbSet<RolOdiSoruCevapSecenek> RolOdiSoruCevapSecenekleri { get; set; }
        public DbSet<RolOdiSoruAciklama> RolOdiSoruAciklamalar { get; set; }

        //Kayıtlı Rol Modelleri
        public DbSet<KayitliRolOdiFotoOrnekFotograf> KayitliRolOdiFotoOrnekFotograflar { get; set; }
        public DbSet<KayitliRolOdiFotoPoz> KayitliRolOdiFotoPozlar { get; set; }
        public DbSet<KayitliRolOdiFotoYonetmenNotu> KayitliRolOdiFotoYonetmenNotlari { get; set; }
        public DbSet<KayitliRolOdiSes> KayitliRolOdiSesler { get; set; }
        public DbSet<KayitliRolOdiSesYonetmenNotu> KayitliRolOdiSesYonetmenNotlari { get; set; }
        public DbSet<KayitliRolOdiSesSenaryo> KayitliRolOdiSesSenaryolari { get; set; }
        public DbSet<KayitliRolOdiSoru> KayitliRolOdiSorular { get; set; }
        public DbSet<KayitliRolOdiSoruAciklama> KayitliRolOdiSoruAciklamalar { get; set; }
        public DbSet<KayitliRolOdiSoruCevapSecenek> KayitliRolOdiSoruCevapSecenekler { get; set; }
        public DbSet<KayitliRolOdiVideo> KayitliRolOdiVideolar { get; set; }
        public DbSet<KayitliRolOdiVideoDetay> KayitliRolOdiVideoDetaylar { get; set; }
        public DbSet<KayitliRolOdiVideoOrnekOyun> KayitliRolOdiVideoOrnekOyunlar { get; set; }
        public DbSet<KayitliRolOdiVideoSenaryo> KayitliRolOdiVideoSenaryolar { get; set; }
        public DbSet<KayitliRolOdiVideoYonetmenNotu> KayitliRolOdiVideoYonetmenNotlar { get; set; }
        public DbSet<KayitliRol> KayitliRoller { get; set; }
        public DbSet<KayitliRolAnketSorusu> KayitliRolAnketSorulari { get; set; }
        public DbSet<KayitliRolOzellik> KayitliRolOzellikler { get; set; }
        public DbSet<KayitliRolOdi> KayitliRolOdiler { get; set; }

        #endregion

        #region Uygulama Bilgileri

        public DbSet<Dil> Diller { get; set; }
        public DbSet<TelefonKodu> TelefonKodlari { get; set; }
        public DbSet<KayitTuru> KayitTurleri { get; set; }
        public DbSet<KayitGrubu> KayitGruplari { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<SosyalMedya> SosyalMedyalar { get; set; }
        public DbSet<Banka> Bankalar { get; set; }
        public DbSet<SabitMetin> SabitMetinler { get; set; }
        public DbSet<SSS> SSSLer { get; set; }
        public DbSet<Ulke> Ulkeler { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value,
                    b => b.MigrationsAssembly("OdiApp.DataAccessLayer"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Additional model configuration
        }

        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            // Kullanıcı girişi kontrolü
            if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated != true)
            {
                return; // Kullanıcı giriş yapmamışsa audit işlemlerini yapma
            }

            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseAuditModel && (e.State == EntityState.Added || e.State == EntityState.Modified));

            // Eğer audit edilecek entity yoksa metoddan çık
            if (!entries.Any())
            {
                return;
            }

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseAuditModel)entry.Entity).EklenmeTarihi = DateTime.UtcNow;
                    ((BaseAuditModel)entry.Entity).EkleyenId = userId;
                    ((BaseAuditModel)entry.Entity).Ekleyen = userName;
                }
                else if (entry.State == EntityState.Modified)
                {
                    ((BaseAuditModel)entry.Entity).GuncellenmeTarihi = DateTime.UtcNow;
                    ((BaseAuditModel)entry.Entity).GuncelleyenId = userId;
                    ((BaseAuditModel)entry.Entity).Guncelleyen = userName;
                }
            }
        }
    }
}