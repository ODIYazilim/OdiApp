using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSes;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiSoru;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliOdiVideo;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolBilgisi;
using OdiApp.EntityLayer.ProjelerModels.KayitliModels.KayitliRolOdi;

namespace OdiApp.DataAccessLayer.ProjelerDataServices.KayitliRoller
{
    public class KayitliRolDataService : IKayitliRolDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public KayitliRolDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<KayitliRolOdiFotoOrnekFotograf> YeniKayitliRolOdiFotoOrnekFotograf(KayitliRolOdiFotoOrnekFotograf model)
        {
            await _dbContext.KayitliRolOdiFotoOrnekFotograflar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiFotoOrnekFotograf>> YeniKayitliRolOdiFotoOrnekFotograf(List<KayitliRolOdiFotoOrnekFotograf> model)
        {
            await _dbContext.KayitliRolOdiFotoOrnekFotograflar.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiFotoPoz> YeniKayitliRolOdiFotoPoz(KayitliRolOdiFotoPoz model)
        {
            await _dbContext.KayitliRolOdiFotoPozlar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiFotoPoz>> YeniKayitliRolOdiFotoPoz(List<KayitliRolOdiFotoPoz> model)
        {
            await _dbContext.KayitliRolOdiFotoPozlar.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiFotoYonetmenNotu> YeniKayitliRolOdiFotoYonetmenNotu(KayitliRolOdiFotoYonetmenNotu model)
        {
            await _dbContext.KayitliRolOdiFotoYonetmenNotlari.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiFotoYonetmenNotu>> YeniKayitliRolOdiFotoYonetmenNotu(List<KayitliRolOdiFotoYonetmenNotu> model)
        {
            await _dbContext.KayitliRolOdiFotoYonetmenNotlari.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiSes> YeniKayitliRolOdiSes(KayitliRolOdiSes model)
        {
            await _dbContext.KayitliRolOdiSesler.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiSes>> YeniKayitliRolOdiSes(List<KayitliRolOdiSes> model)
        {
            await _dbContext.KayitliRolOdiSesler.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiSesYonetmenNotu> YeniKayitliRolOdiSesYonetmenNotu(KayitliRolOdiSesYonetmenNotu model)
        {
            await _dbContext.KayitliRolOdiSesYonetmenNotlari.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiSesYonetmenNotu>> YeniKayitliRolOdiSesYonetmenNotu(List<KayitliRolOdiSesYonetmenNotu> model)
        {
            await _dbContext.KayitliRolOdiSesYonetmenNotlari.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiSesSenaryo> YeniKayitliRolOdiSesSenaryo(KayitliRolOdiSesSenaryo model)
        {
            await _dbContext.KayitliRolOdiSesSenaryolari.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiSesSenaryo>> YeniKayitliRolOdiSesSenaryo(List<KayitliRolOdiSesSenaryo> model)
        {
            await _dbContext.KayitliRolOdiSesSenaryolari.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiSoru> YeniKayitliRolOdiSoru(KayitliRolOdiSoru model)
        {
            await _dbContext.KayitliRolOdiSorular.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiSoru>> YeniKayitliRolOdiSoru(List<KayitliRolOdiSoru> model)
        {
            await _dbContext.KayitliRolOdiSorular.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiSoruAciklama> YeniKayitliRolOdiSoruAciklama(KayitliRolOdiSoruAciklama model)
        {
            await _dbContext.KayitliRolOdiSoruAciklamalar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiSoruAciklama>> YeniKayitliRolOdiSoruAciklama(List<KayitliRolOdiSoruAciklama> model)
        {
            await _dbContext.KayitliRolOdiSoruAciklamalar.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiSoruCevapSecenek> YeniKayitliRolOdiSoruCevapSecenek(KayitliRolOdiSoruCevapSecenek model)
        {
            await _dbContext.KayitliRolOdiSoruCevapSecenekler.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiVideo> YeniKayitliRolOdiVideo(KayitliRolOdiVideo model)
        {
            await _dbContext.KayitliRolOdiVideolar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiVideo>> YeniKayitliRolOdiVideo(List<KayitliRolOdiVideo> model)
        {
            await _dbContext.KayitliRolOdiVideolar.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiVideoDetay> YeniKayitliRolOdiVideoDetay(KayitliRolOdiVideoDetay model)
        {
            await _dbContext.KayitliRolOdiVideoDetaylar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiVideoOrnekOyun> YeniKayitliRolOdiVideoOrnekOyun(KayitliRolOdiVideoOrnekOyun model)
        {
            await _dbContext.KayitliRolOdiVideoOrnekOyunlar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiVideoOrnekOyun>> YeniKayitliRolOdiVideoOrnekOyun(List<KayitliRolOdiVideoOrnekOyun> model)
        {
            await _dbContext.KayitliRolOdiVideoOrnekOyunlar.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiVideoSenaryo> YeniKayitliRolOdiVideoSenaryo(KayitliRolOdiVideoSenaryo model)
        {
            await _dbContext.KayitliRolOdiVideoSenaryolar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiVideoSenaryo>> YeniKayitliRolOdiVideoSenaryo(List<KayitliRolOdiVideoSenaryo> model)
        {
            await _dbContext.KayitliRolOdiVideoSenaryolar.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdiVideoYonetmenNotu> YeniKayitliRolOdiVideoYonetmenNotu(KayitliRolOdiVideoYonetmenNotu model)
        {
            await _dbContext.KayitliRolOdiVideoYonetmenNotlar.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<KayitliRolOdiVideoYonetmenNotu>> YeniKayitliRolOdiVideoYonetmenNotu(List<KayitliRolOdiVideoYonetmenNotu> model)
        {
            await _dbContext.KayitliRolOdiVideoYonetmenNotlar.AddRangeAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRol> YeniKayitliRol(KayitliRol model)
        {
            await _dbContext.KayitliRoller.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolAnketSorusu> YeniKayitliRolAnketSorusu(KayitliRolAnketSorusu model)
        {
            await _dbContext.KayitliRolAnketSorulari.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOzellik> YeniKayitliRolOzellik(KayitliRolOzellik model)
        {
            await _dbContext.KayitliRolOzellikler.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<KayitliRolOdi> YeniKayitliRolOdi(KayitliRolOdi model)
        {
            await _dbContext.KayitliRolOdiler.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }
    }
}