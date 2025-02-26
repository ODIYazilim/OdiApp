using OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.ProfilOnayDataServices;

public interface IProfilOnayDataService
{
    //performer
    Task<ProfilOnay> ProfilOnayaGonder(ProfilOnay onay);
    Task<List<ProfilOnay>> ProfilOnaySureci(string oyuncuId);
    Task<ProfilOnay> SonOnayKaydiGetir(string performerId);

    //menajer
    Task<ProfilOnay> ProfilOnayGuncelle(ProfilOnay profilOnay);
    Task<ProfilOnay> ProfilOnayGetir(string profilOnayId);
    Task<ProfilOnay> ProfilOnaySonDurumGetir(string performerId);

    Task<bool> AcikOnaySureciVarmi(string oyuncuId);

    //listeler
    //Task<List<ProfilOnay>> AcikTalepListesi(string menajerId);
    Task<int> AcikTalepSayisi(string menajerId);
    //Task<List<ProfilOnay>> RedProfilOnayListesi(string menajerId);
    Task<int> RedTalepSayisi(string menajerId);
    Task<List<ProfilOnay>> OnayliProfilOnayListesi(string menajerId);
    Task<int> OnayliProfilSayisi(string menajerId);

    //profil onay red nedeni tanımı
    Task<ProfilOnayRedNedeniTanimi> ProfilOnayRedNedeniTanimiEkle(ProfilOnayRedNedeniTanimi model);
    Task<ProfilOnayRedNedeniTanimi> ProfilOnayRedNedeniTanimiGuncelle(ProfilOnayRedNedeniTanimi model);
    Task<ProfilOnayRedNedeniTanimi> ProfilOnayRedNedeniTanimiGetir(int modelId);
    Task<bool> ProfilOnayRedNedeniTanimiSil(ProfilOnayRedNedeniTanimi model);
    Task<List<ProfilOnayRedNedeniTanimi>> ProfilOnayRedNedeniTanimiListe();
    Task<List<ProfilOnayRedNedeniTanimi>> ProfilOnayRedNedeniTanimiListe(List<int> idList);

    //profil onay red nedeni
    Task<ProfilOnayRedNedeni> ProfilOnayRedNedeniEkle(ProfilOnayRedNedeni model);
    Task<List<ProfilOnayRedNedeni>> ProfilOnayRedNedeniTopluEkle(List<ProfilOnayRedNedeni> list);
    Task<bool> ProfilOnayRedNedeniSil(ProfilOnayRedNedeni model);
    Task<List<ProfilOnayRedNedeni>> ProfilOnayRedNedeniListe(string profilOnayId);
    Task<List<ProfilOnayRedNedeni>> ProfilOnayRedNedeniListe(List<string> profilOnayIdListesi);
}