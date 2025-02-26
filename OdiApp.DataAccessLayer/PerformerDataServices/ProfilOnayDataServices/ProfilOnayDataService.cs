using Microsoft.EntityFrameworkCore;
using OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.ProfilOnayDataServices;

public class ProfilOnayDataService : IProfilOnayDataService
{
    private readonly ApplicationDbContext _dbContext;

    public ProfilOnayDataService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region PERFORMER

    public async Task<ProfilOnay> ProfilOnayaGonder(ProfilOnay onay)
    {
        await _dbContext.ProfilOnaylari.AddAsync(onay);
        await _dbContext.SaveChangesAsync();

        return onay;
    }

    public async Task<List<ProfilOnay>> ProfilOnaySureci(string oyuncuId)
    {
        return await _dbContext.ProfilOnaylari.Where(x => x.PerformerId == oyuncuId).OrderByDescending(x => x.OnayGonderimTarihi).ToListAsync();
    }

    public async Task<ProfilOnay> SonOnayKaydiGetir(string performerId)
    {
        return await _dbContext.ProfilOnaylari.Where(x => x.PerformerId == performerId).OrderByDescending(x => x.GuncellenmeTarihi).FirstOrDefaultAsync();
    }

    #endregion

    #region MENAJER

    public async Task<ProfilOnay> ProfilOnayGuncelle(ProfilOnay profilOnay)
    {
        _dbContext.ProfilOnaylari.Update(profilOnay);
        await _dbContext.SaveChangesAsync();

        return profilOnay;
    }

    public async Task<bool> AcikOnaySureciVarmi(string oyuncuId)
    {
        ProfilOnay onay = await _dbContext.ProfilOnaylari.FirstOrDefaultAsync(x => x.PerformerId == oyuncuId && x.Aktif == true);
        if (onay == null) return false;
        else return true;
    }

    //public async Task<List<ProfilOnay>> AcikTalepListesi(string menajerId)
    //{
    //    return await _dbContext.ProfilOnaylari.Where(x => x.YetenekTemsilcisiId == menajerId && x.Aktif == true).ToListAsync();
    //}

    public async Task<int> AcikTalepSayisi(string menajerId)
    {
        return await _dbContext.ProfilOnaylari.Where(x => x.YetenekTemsilcisiId == menajerId && x.Aktif == true).CountAsync();
    }

    //public async Task<List<ProfilOnay>> RedProfilOnayListesi(string menajerId)
    //{
    //    return await _dbContext.ProfilOnaylari.Where(x => x.YetenekTemsilcisiId == menajerId && x.Aktif == false && x.Red == true).ToListAsync();
    //}

    public async Task<int> RedTalepSayisi(string menajerId)
    {
        return await _dbContext.ProfilOnaylari.Where(x => x.YetenekTemsilcisiId == menajerId && x.Aktif == false && x.Red == true).CountAsync();
    }

    public async Task<List<ProfilOnay>> OnayliProfilOnayListesi(string menajerId)
    {
        return await _dbContext.ProfilOnaylari.Where(x => x.YetenekTemsilcisiId == menajerId && x.Aktif == false && x.Onay == true).ToListAsync();
    }

    public async Task<int> OnayliProfilSayisi(string menajerId)
    {
        return await _dbContext.ProfilOnaylari.Where(x => x.YetenekTemsilcisiId == menajerId && x.Aktif == false && x.Onay == true).CountAsync();
    }

    public async Task<ProfilOnay> ProfilOnayGetir(string profilOnayId)
    {
        return await _dbContext.ProfilOnaylari.FirstOrDefaultAsync(x => x.Id == profilOnayId);
    }

    public async Task<ProfilOnay> ProfilOnaySonDurumGetir(string performerId)
    {
        return await _dbContext.ProfilOnaylari.Where(x => x.PerformerId == performerId).OrderByDescending(x => x.GuncellenmeTarihi).FirstOrDefaultAsync();
    }

    #endregion

    #region Profil Onay Red Nedeni Tanımı

    public async Task<ProfilOnayRedNedeniTanimi> ProfilOnayRedNedeniTanimiEkle(ProfilOnayRedNedeniTanimi model)
    {
        await _dbContext.ProfilOnayRedNedeniTanimlari.AddAsync(model);
        await _dbContext.SaveChangesAsync();

        return model;
    }

    public async Task<ProfilOnayRedNedeniTanimi> ProfilOnayRedNedeniTanimiGuncelle(ProfilOnayRedNedeniTanimi model)
    {
        _dbContext.ProfilOnayRedNedeniTanimlari.Update(model);
        await _dbContext.SaveChangesAsync();

        return model;
    }

    public async Task<ProfilOnayRedNedeniTanimi> ProfilOnayRedNedeniTanimiGetir(int id)
    {
        return await _dbContext.ProfilOnayRedNedeniTanimlari.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<ProfilOnayRedNedeniTanimi>> ProfilOnayRedNedeniTanimiListe()
    {
        return await _dbContext.ProfilOnayRedNedeniTanimlari.AsNoTracking().Where(x => x.Aktif).ToListAsync();
    }

    public async Task<List<ProfilOnayRedNedeniTanimi>> ProfilOnayRedNedeniTanimiListe(List<int> idList)
    {
        return await _dbContext.ProfilOnayRedNedeniTanimlari.AsNoTracking().Where(x => idList.Contains(x.Id)).ToListAsync();
    }

    public async Task<bool> ProfilOnayRedNedeniTanimiSil(ProfilOnayRedNedeniTanimi model)
    {
        _dbContext.ProfilOnayRedNedeniTanimlari.Remove(model);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Profil Onay Red Nedeni

    public async Task<ProfilOnayRedNedeni> ProfilOnayRedNedeniEkle(ProfilOnayRedNedeni model)
    {
        await _dbContext.ProfilOnayRedNedenleri.AddAsync(model);
        await _dbContext.SaveChangesAsync();

        return model;
    }

    public async Task<List<ProfilOnayRedNedeni>> ProfilOnayRedNedeniTopluEkle(List<ProfilOnayRedNedeni> list)
    {
        await _dbContext.ProfilOnayRedNedenleri.AddRangeAsync(list);
        await _dbContext.SaveChangesAsync();

        return list;
    }

    public async Task<bool> ProfilOnayRedNedeniSil(ProfilOnayRedNedeni model)
    {
        _dbContext.ProfilOnayRedNedenleri.Remove(model);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<ProfilOnayRedNedeni>> ProfilOnayRedNedeniListe(string profilOnayId)
    {
        return await _dbContext.ProfilOnayRedNedenleri.AsNoTracking().Where(x => x.ProfilOnayId == profilOnayId).ToListAsync();
    }

    public async Task<List<ProfilOnayRedNedeni>> ProfilOnayRedNedeniListe(List<string> profilOnayId)
    {
        return await _dbContext.ProfilOnayRedNedenleri.AsNoTracking().Where(x => profilOnayId.Contains(x.ProfilOnayId)).ToListAsync();
    }

    #endregion
}