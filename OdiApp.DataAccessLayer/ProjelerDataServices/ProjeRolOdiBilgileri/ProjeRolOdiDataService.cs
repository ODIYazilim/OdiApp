using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.ProjelerModels.OdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.OdiSes;
using OdiApp.EntityLayer.ProjelerModels.OdiSoru;
using OdiApp.EntityLayer.ProjelerModels.OdiVideo;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolOdi;

namespace OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolOdiBilgileri
{
    public class ProjeRolOdiDataService : IProjeRolOdiDataService
    {
        ApplicationDbContext _dbContext;

        public ProjeRolOdiDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region PROJE ROL ODİ

        public async Task<ProjeRolOdi> ProjeRolOdiGetir(string projeRolId)
        {
            return await _dbContext.ProjeRolOdileri.AsNoTracking().FirstOrDefaultAsync(x => x.ProjeRolId == projeRolId);
        }

        public async Task<List<ProjeRolOdi>> ProjeRolOdiListeGetir(string projeRolId)
        {
            return await _dbContext.ProjeRolOdileri.AsNoTracking().Where(x => x.ProjeRolId == projeRolId).ToListAsync();
        }

        public async Task<ProjeRolOdi> YeniProjeRolOdi(ProjeRolOdi odi)
        {
            await _dbContext.ProjeRolOdileri.AddAsync(odi);
            await _dbContext.SaveChangesAsync();
            return odi;
        }

        public async Task<List<ProjeRolOdi>> YeniProjeRolOdi(List<ProjeRolOdi> projeRolOdiListe)
        {
            await _dbContext.ProjeRolOdileri.AddRangeAsync(projeRolOdiListe);
            await _dbContext.SaveChangesAsync();
            return projeRolOdiListe;
        }

        public async Task<ProjeRolOdi> ProjeRolOdiGuncelle(ProjeRolOdi odi)
        {
            _dbContext.ProjeRolOdileri.Update(odi);
            await _dbContext.SaveChangesAsync();
            return odi;
        }

        #endregion

        #region FOTOMATİK
        //poz
        public async Task<RolOdiFotoPoz> YeniRolOdiFotoPoz(RolOdiFotoPoz odiFoto)
        {
            await _dbContext.RolOdiFotoPozlar.AddAsync(odiFoto);
            await _dbContext.SaveChangesAsync();
            return odiFoto;
        }
        public async Task<List<RolOdiFotoPoz>> YeniRolOdiFotoPozList(List<RolOdiFotoPoz> odiFotoList)
        {
            await _dbContext.RolOdiFotoPozlar.AddRangeAsync(odiFotoList);
            await _dbContext.SaveChangesAsync();
            return odiFotoList;
        }
        public async Task<List<RolOdiFotoPoz>> RolOdiFotoPozListesiGuncelle(List<RolOdiFotoPoz> list)
        {
            _dbContext.RolOdiFotoPozlar.UpdateRange(list);
            await _dbContext.SaveChangesAsync();
            return list;
        }
        public async Task<bool> RolOdiFotoPozListesiSil(List<RolOdiFotoPoz> odiFotoList)
        {
            _dbContext.RolOdiFotoPozlar.RemoveRange(odiFotoList);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        //yonetmen notu

        public async Task<RolOdiFotoYonetmenNotu> RolOdiFotoYonetmenNotuGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiFotoYonetmenNotlari.AsNoTracking().FirstOrDefaultAsync(x => x.ProjeRolOdiId == rolOdiId);
        }

        public async Task<List<RolOdiFotoYonetmenNotu>> RolOdiFotoYonetmenNotuListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiFotoYonetmenNotlari.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }

        public async Task<RolOdiFotoYonetmenNotu> YeniRolOdiFotoYonetmenNotu(RolOdiFotoYonetmenNotu yonetmenNotu)
        {
            await _dbContext.RolOdiFotoYonetmenNotlari.AddAsync(yonetmenNotu);
            await _dbContext.SaveChangesAsync();
            return yonetmenNotu;
        }

        public async Task<List<RolOdiFotoYonetmenNotu>> YeniRolOdiFotoYonetmenNotu(List<RolOdiFotoYonetmenNotu> yonetmenNotuList)
        {
            await _dbContext.RolOdiFotoYonetmenNotlari.AddRangeAsync(yonetmenNotuList);
            await _dbContext.SaveChangesAsync();
            return yonetmenNotuList;
        }

        public async Task<RolOdiFotoYonetmenNotu> OdiRolFotoYonetmenNotuGuncelle(RolOdiFotoYonetmenNotu yonetmenNotu)
        {
            _dbContext.RolOdiFotoYonetmenNotlari.Update(yonetmenNotu);
            await _dbContext.SaveChangesAsync();
            return yonetmenNotu;
        }

        // örnek foto
        public async Task<List<RolOdiFotoOrnekFotograf>> RolOdiFotoOrnekFotoListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiFotoOrnekFotograflar.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }
        public async Task<List<RolOdiFotoOrnekFotograf>> YeniRolOdiFotoOrnekFotolar(List<RolOdiFotoOrnekFotograf> ornekFotoListesi)
        {
            await _dbContext.RolOdiFotoOrnekFotograflar.AddRangeAsync(ornekFotoListesi);
            await _dbContext.SaveChangesAsync();
            return ornekFotoListesi;
        }
        public async Task<bool> RolOdiOrnekFotoListesiSil(List<RolOdiFotoOrnekFotograf> ornekFotoListesi)
        {
            _dbContext.RolOdiFotoOrnekFotograflar.RemoveRange(ornekFotoListesi);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<RolOdiFotoOrnekFotograf>> RolOdiFotoOrnekFotoListeGuncelle(List<RolOdiFotoOrnekFotograf> ornekFotoListesi)
        {
            _dbContext.RolOdiFotoOrnekFotograflar.UpdateRange(ornekFotoListesi);
            await _dbContext.SaveChangesAsync();
            return ornekFotoListesi;
        }
        public async Task<List<RolOdiFotoPoz>> RolOdiFotoPozListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiFotoPozlar.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }

        #endregion

        #region SESMATİK

        //ses
        public async Task<RolOdiSes> YeniRolOdiSes(RolOdiSes odiSes)
        {
            await _dbContext.RolOdiSesler.AddAsync(odiSes);
            await _dbContext.SaveChangesAsync();
            return odiSes;
        }
        public async Task<List<RolOdiSes>> YeniRolOdiSesList(List<RolOdiSes> odiSesList)
        {
            await _dbContext.RolOdiSesler.AddRangeAsync(odiSesList);
            await _dbContext.SaveChangesAsync();
            return odiSesList;
        }
        public async Task<List<RolOdiSes>> RolOdiSesListesiGuncelle(List<RolOdiSes> list)
        {
            _dbContext.RolOdiSesler.UpdateRange(list);
            await _dbContext.SaveChangesAsync();
            return list;
        }

        public async Task<bool> RolOdiSesListesiSil(List<RolOdiSes> odiSesList)
        {
            _dbContext.RolOdiSesler.RemoveRange(odiSesList);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<RolOdiSes>> RolOdiSesListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiSesler.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }

        //yonetmen Notu
        public async Task<RolOdiSesYonetmenNotu> YeniRolOdiSesYonetmenNotu(RolOdiSesYonetmenNotu yonetmenNotu)
        {
            await _dbContext.RolOdiSesYonetmenNotlari.AddAsync(yonetmenNotu);
            await _dbContext.SaveChangesAsync();
            return yonetmenNotu;
        }

        public async Task<List<RolOdiSesYonetmenNotu>> YeniRolOdiSesYonetmenNotu(List<RolOdiSesYonetmenNotu> yonetmenNotuList)
        {
            await _dbContext.RolOdiSesYonetmenNotlari.AddRangeAsync(yonetmenNotuList);
            await _dbContext.SaveChangesAsync();
            return yonetmenNotuList;
        }

        public async Task<RolOdiSesYonetmenNotu> OdiRolSesYonetmenNotuGuncelle(RolOdiSesYonetmenNotu yonetmenNotu)
        {
            _dbContext.RolOdiSesYonetmenNotlari.Update(yonetmenNotu);
            await _dbContext.SaveChangesAsync();
            return yonetmenNotu;
        }
        public async Task<RolOdiSesYonetmenNotu> RolOdiSesYonetmenNotuGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiSesYonetmenNotlari.FirstOrDefaultAsync(x => x.ProjeRolOdiId == rolOdiId);
        }

        public async Task<List<RolOdiSesYonetmenNotu>> RolOdiSesYonetmenNotuListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiSesYonetmenNotlari.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }

        //senaryo
        public async Task<RolOdiSesSenaryo> YeniRolOdiSesSenaryo(RolOdiSesSenaryo senaryo)
        {
            await _dbContext.RolOdiSesSenaryolar.AddAsync(senaryo);
            await _dbContext.SaveChangesAsync();
            return senaryo;
        }

        public async Task<List<RolOdiSesSenaryo>> YeniRolOdiSesSenaryo(List<RolOdiSesSenaryo> senaryoList)
        {
            await _dbContext.RolOdiSesSenaryolar.AddRangeAsync(senaryoList);
            await _dbContext.SaveChangesAsync();
            return senaryoList;
        }

        public async Task<RolOdiSesSenaryo> RolOdiSesSenaryoGuncelle(RolOdiSesSenaryo senaryo)
        {
            _dbContext.RolOdiSesSenaryolar.Update(senaryo);
            await _dbContext.SaveChangesAsync();
            return senaryo;
        }
        public async Task<bool> RolOdiSesSenaryoSil(string senaryoId)
        {
            RolOdiSesSenaryo senaryo = await _dbContext.RolOdiSesSenaryolar.AsNoTracking().FirstOrDefaultAsync(x => x.Id == senaryoId);
            if (senaryo == null) { return false; }
            else
            {
                _dbContext.RolOdiSesSenaryolar.Remove(senaryo);
                _dbContext.SaveChanges();
                return true;
            }
        }
        public async Task<RolOdiSesSenaryo> RolOdiSesSenaryoGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiSesSenaryolar.AsNoTracking().FirstOrDefaultAsync(x => x.ProjeRolOdiId == rolOdiId);
        }

        public async Task<List<RolOdiSesSenaryo>> RolOdiSesSenaryoListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiSesSenaryolar.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }

        #endregion

        #region VİDEOMATİK
        public async Task<RolOdiVideo> YeniRolOdiVideo(RolOdiVideo video)
        {
            await _dbContext.RolOdiVideolar.AddAsync(video);
            await _dbContext.SaveChangesAsync();
            return video;
        }

        public async Task<List<RolOdiVideo>> YeniRolOdiVideo(List<RolOdiVideo> videoList)
        {
            await _dbContext.RolOdiVideolar.AddRangeAsync(videoList);
            await _dbContext.SaveChangesAsync();
            return videoList;
        }
        public async Task<RolOdiVideo> RolOdiVideoGuncelle(RolOdiVideo video)
        {
            _dbContext.RolOdiVideolar.Update(video);
            await _dbContext.SaveChangesAsync();
            return video;
        }
        public async Task<RolOdiVideo> RolOdiVideoGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiVideolar.Include(x => x.DetayList).AsNoTracking().FirstOrDefaultAsync(x => x.ProjeRolOdiId == rolOdiId);
        }
        public async Task<List<RolOdiVideo>> RolOdiVideoListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiVideolar.Include(x => x.DetayList).Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }
        public async Task<List<RolOdiVideoDetay>> YeniRolOdiVideoDetayList(List<RolOdiVideoDetay> detayList)
        {
            await _dbContext.RolOdiVideoDetaylar.AddRangeAsync(detayList);
            await _dbContext.SaveChangesAsync();
            return detayList;
        }
        public async Task<List<RolOdiVideoDetay>> RolOdiVideoDetayListGuncelle(List<RolOdiVideoDetay> detayList)
        {
            _dbContext.RolOdiVideoDetaylar.UpdateRange(detayList);
            await _dbContext.SaveChangesAsync();
            return detayList;
        }
        public async Task<bool> RolOdiVideoDetayListSil(List<RolOdiVideoDetay> detayList)
        {
            _dbContext.RolOdiVideoDetaylar.RemoveRange(detayList);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<RolOdiVideoDetay>> RolOdiVideoDetayListGetir(string videoId)
        {
            return await _dbContext.RolOdiVideoDetaylar.Where(x => x.RolOdiVideoId == videoId).AsNoTracking().ToListAsync();
        }
        public async Task<RolOdiVideoOrnekOyun> YeniRolOdiVideoOrnekOyun(RolOdiVideoOrnekOyun oyun)
        {
            await _dbContext.RolOdiVideoOrnekOyunlar.AddAsync(oyun);
            await _dbContext.SaveChangesAsync();
            return oyun;
        }
        public async Task<List<RolOdiVideoOrnekOyun>> YeniRolOdiVideoOrnekOyunList(List<RolOdiVideoOrnekOyun> oyunList)
        {
            await _dbContext.RolOdiVideoOrnekOyunlar.AddRangeAsync(oyunList);
            await _dbContext.SaveChangesAsync();
            return oyunList;
        }
        public async Task<List<RolOdiVideoOrnekOyun>> RolOdiVideoOrnekOyunListGuncelle(List<RolOdiVideoOrnekOyun> oyunList)
        {
            _dbContext.RolOdiVideoOrnekOyunlar.UpdateRange(oyunList);
            await _dbContext.SaveChangesAsync();
            return oyunList;
        }
        public async Task<RolOdiVideoOrnekOyun> RolOdiVideoOrnekOyunGuncelle(RolOdiVideoOrnekOyun oyun)
        {
            _dbContext.RolOdiVideoOrnekOyunlar.Update(oyun);
            await _dbContext.SaveChangesAsync();
            return oyun;
        }
        public async Task<RolOdiVideoOrnekOyun> RolOdiVideoOrnekOyunGetir(string oyunId)
        {
            return await _dbContext.RolOdiVideoOrnekOyunlar.AsNoTracking().FirstOrDefaultAsync(x => x.Id == oyunId);
        }
        public async Task<List<RolOdiVideoOrnekOyun>> RolOdiVideoOrnekOyunListGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiVideoOrnekOyunlar.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }
        public async Task<bool> RolOdiVideoOrnekOyunListSil(List<RolOdiVideoOrnekOyun> oyunList)
        {
            _dbContext.RolOdiVideoOrnekOyunlar.RemoveRange(oyunList);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RolOdiVideoOrnekOyunSil(string oyunId)
        {
            RolOdiVideoOrnekOyun oyun = await RolOdiVideoOrnekOyunGetir(oyunId);
            if (oyun == null) return false;
            _dbContext.RolOdiVideoOrnekOyunlar.Remove(oyun);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<RolOdiVideoSenaryo> YeniRolOdiVideoSenaryo(RolOdiVideoSenaryo senaryo)
        {
            await _dbContext.RolOdiVideoSenaryolar.AddAsync(senaryo);
            await _dbContext.SaveChangesAsync();
            return senaryo;
        }

        public async Task<List<RolOdiVideoSenaryo>> YeniRolOdiVideoSenaryo(List<RolOdiVideoSenaryo> senaryoList)
        {
            await _dbContext.RolOdiVideoSenaryolar.AddRangeAsync(senaryoList);
            await _dbContext.SaveChangesAsync();
            return senaryoList;
        }

        public async Task<RolOdiVideoSenaryo> RolOdiVideoSenaryoGuncelle(RolOdiVideoSenaryo senaryo)
        {
            _dbContext.RolOdiVideoSenaryolar.Update(senaryo);
            await _dbContext.SaveChangesAsync();
            return senaryo;
        }

        public async Task<RolOdiVideoSenaryo> RolOdiVideoSenaryoGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiVideoSenaryolar.AsNoTracking().FirstOrDefaultAsync(x => x.ProjeRolOdiId == rolOdiId);
        }

        public async Task<List<RolOdiVideoSenaryo>> RolOdiVideoSenaryoListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiVideoSenaryolar.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }

        public async Task<RolOdiVideoYonetmenNotu> YeniRolOdiVideoYonetmenNotu(RolOdiVideoYonetmenNotu not)
        {
            await _dbContext.RolOdiVideoYonetmenNotlari.AddAsync(not);
            await _dbContext.SaveChangesAsync();
            return not;
        }

        public async Task<List<RolOdiVideoYonetmenNotu>> YeniRolOdiVideoYonetmenNotu(List<RolOdiVideoYonetmenNotu> notList)
        {
            await _dbContext.RolOdiVideoYonetmenNotlari.AddRangeAsync(notList);
            await _dbContext.SaveChangesAsync();
            return notList;
        }

        public async Task<RolOdiVideoYonetmenNotu> RolOdiVideoYonetmenNotuGuncelle(RolOdiVideoYonetmenNotu not)
        {
            _dbContext.RolOdiVideoYonetmenNotlari.Update(not);
            await _dbContext.SaveChangesAsync();
            return not;
        }

        public async Task<RolOdiVideoYonetmenNotu> RolOdiVideoYonetmenNotuGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiVideoYonetmenNotlari.AsNoTracking().FirstOrDefaultAsync(x => x.ProjeRolOdiId == rolOdiId);
        }

        public async Task<List<RolOdiVideoYonetmenNotu>> RolOdiVideoYonetmenNotuListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiVideoYonetmenNotlari.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }

        #endregion

        #region SORUMATİK
        public async Task<RolOdiSoru> YeniRolOdiSoru(RolOdiSoru soru)
        {
            await _dbContext.RolOdiSorular.AddAsync(soru);
            await _dbContext.SaveChangesAsync();
            return soru;
        }
        public async Task<List<RolOdiSoru>> YeniRolOdiSoruList(List<RolOdiSoru> soruList)
        {
            await _dbContext.RolOdiSorular.AddRangeAsync(soruList);
            await _dbContext.SaveChangesAsync();
            return soruList;
        }
        public async Task<RolOdiSoru> RolOdiSoruGuncelle(RolOdiSoru soru)
        {
            _dbContext.RolOdiSorular.Update(soru);
            await _dbContext.SaveChangesAsync();
            return soru;
        }
        public async Task<List<RolOdiSoru>> RolOdiSoruListGuncelle(List<RolOdiSoru> soruList)
        {
            _dbContext.RolOdiSorular.UpdateRange(soruList);
            await _dbContext.SaveChangesAsync();
            return soruList;
        }

        public async Task<bool> RolOdiSoruSil(string soruId)
        {
            RolOdiSoru soru = await _dbContext.RolOdiSorular.FirstOrDefaultAsync(x => x.Id == soruId);
            if (soru == null) return false;
            _dbContext.Remove(soru);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RolOdiSoruListSil(List<RolOdiSoru> soruList)
        {
            _dbContext.RolOdiSorular.RemoveRange(soruList);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<RolOdiSoru> RolOdiSoruGetir(string soruId)
        {
            return await _dbContext.RolOdiSorular.Include(x => x.CevapSecenekleri).AsNoTracking().FirstOrDefaultAsync(x => x.Id == soruId);
        }
        public async Task<List<RolOdiSoru>> RolOdiSoruListGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiSorular.Include(x => x.CevapSecenekleri).Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }
        public async Task<List<RolOdiSoruCevapSecenek>> YeniRolOdiSoruCevapList(List<RolOdiSoruCevapSecenek> soruList)
        {
            await _dbContext.RolOdiSoruCevapSecenekleri.AddRangeAsync(soruList);
            await _dbContext.SaveChangesAsync();
            return soruList;
        }
        public async Task<List<RolOdiSoruCevapSecenek>> RolOdiSoruCevapListGuncelle(List<RolOdiSoruCevapSecenek> soruList)
        {
            _dbContext.RolOdiSoruCevapSecenekleri.UpdateRange(soruList);
            await _dbContext.SaveChangesAsync();
            return soruList;
        }
        public async Task<List<RolOdiSoruCevapSecenek>> RolOdiSoruCevapListGetir(string soruId)
        {
            return await _dbContext.RolOdiSoruCevapSecenekleri.Where(x => x.RolOdiSoruId == soruId).AsNoTracking().ToListAsync();
        }
        public async Task<bool> RolOdiSoruCevapListSil(List<RolOdiSoruCevapSecenek> soruCevapList)
        {

            _dbContext.RolOdiSoruCevapSecenekleri.RemoveRange(soruCevapList);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<RolOdiSoruAciklama> YeniRolOdiSoruAciklama(RolOdiSoruAciklama aciklama)
        {
            await _dbContext.RolOdiSoruAciklamalar.AddAsync(aciklama);
            await _dbContext.SaveChangesAsync();
            return aciklama;

        }

        public async Task<List<RolOdiSoruAciklama>> YeniRolOdiSoruAciklama(List<RolOdiSoruAciklama> aciklamaList)
        {
            await _dbContext.RolOdiSoruAciklamalar.AddRangeAsync(aciklamaList);
            await _dbContext.SaveChangesAsync();
            return aciklamaList;

        }
        public async Task<RolOdiSoruAciklama> RolOdiSoruAciklamaGuncelle(RolOdiSoruAciklama aciklama)
        {
            _dbContext.RolOdiSoruAciklamalar.Update(aciklama);
            await _dbContext.SaveChangesAsync();
            return aciklama;
        }
        public async Task<RolOdiSoruAciklama> RolOdiSoruAciklamaGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiSoruAciklamalar.AsNoTracking().FirstOrDefaultAsync(x => x.ProjeRolOdiId == rolOdiId);
        }
        public async Task<List<RolOdiSoruAciklama>> RolOdiSoruAciklamaListesiGetir(string rolOdiId)
        {
            return await _dbContext.RolOdiSoruAciklamalar.Where(x => x.ProjeRolOdiId == rolOdiId).AsNoTracking().ToListAsync();
        }
        #endregion
    }
}
