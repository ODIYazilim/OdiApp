using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.EntityLayer.Base;
using OdiApp.EntityLayer.Identity;
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
using OdiApp.EntityLayer.SharedModels;
using OdiApp.EntityLayer.Token;
using System.Security.Claims;

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
