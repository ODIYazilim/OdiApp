using OdiApp.DTOs.SharedDTOs.ProjeRolPerformerDTOs;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi;

namespace OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolBilgileri
{
    public interface IProjeRolDataService
    {
        Task<ProjeRolOzellik> ProjeRolOzellikGetir(string projeRolId);
        Task<List<ProjeRolOzellik>> ProjeRolOzellikListeGetir(string projeRolId);
        Task<bool> YeniProjeRolOzellik(ProjeRolOzellik ozellik);
        Task<bool> YeniProjeRolOzellik(List<ProjeRolOzellik> ozellikListesi);
        Task<bool> ProjeRolOzellikGuncelle(ProjeRolOzellik ozellik);
        Task<bool> ProjeRolOzellikSil(string projeRolId);

        Task<List<RolOzellikPerformerEtiket>> PerformerEtiketleriGetir(string projeRolOzellikId);
        Task<bool> PerformerEtiketleriEkle(List<RolOzellikPerformerEtiket> performerEtiketleri);
        Task<bool> PerformerEtiketleriSil(List<RolOzellikPerformerEtiket> performerEtiketleri);

        Task<List<RolOzellikFiziksel>> FizikselOzellikleriGetir(string projeRolOzellikId);
        Task<bool> FizikselOzellikleriEkle(List<RolOzellikFiziksel> fizikselOzellikler);
        Task<bool> FizikselOzellikleriSil(List<RolOzellikFiziksel> fizikselOzellikler);

        Task<List<RolOzellikDeneyim>> DeneyimKodlariniGetir(string projeRolOzellikId);
        Task<bool> DeneyimKodlariEkle(List<RolOzellikDeneyim> deneyimKodlari);
        Task<bool> DeneyimKodlariSil(List<RolOzellikDeneyim> deneyimKodlari);

        Task<List<RolOzellikEgitim>> EgitimTipleriniGetir(string projeRolOzellikId);
        Task<bool> EgitimTipleriEkle(List<RolOzellikEgitim> egitimTipleri);
        Task<bool> EgitimTipleriSil(List<RolOzellikEgitim> egitimTipleri);

        Task<List<RolOzellikYetenek>> YetenekTipleriniGetir(string projeRolOzellikId);
        Task<bool> YetenekTipleriEkle(List<RolOzellikYetenek> yetenekTipleri);
        Task<bool> YetenekTipleriSil(List<RolOzellikYetenek> yetenekTipleri);

        Task<ProjeRol> ProjeRolGetir(string projeRolId);
        Task<ProjeRol> ProjeRolFullGetir(string projeRolId);
        Task<ProjeRol> YeniProjeRol(ProjeRol rol);
        Task<ProjeRol> ProjeRolGuncelle(ProjeRol rol);
        Task<List<ProjeRol>> ProjeRolleriGetir(string projeId);
        Task<ProjeRolPerformer> YeniProjeRolPerformer(ProjeRolPerformer model);
        Task<List<ProjeRolPerformerOutputDTO>> ProjeRolPerformerListGetir(string projeId);
        Task<bool> ProjeRolAnketSorusuSil(List<ProjeRolAnketSorusu> anketSorusuListe);
        Task<List<ProjeRolAnketSorusu>> YeniProjeRolAnketSorusu(List<ProjeRolAnketSorusu> anketSorusuListe);
        Task<List<ProjeRolAnketSorusu>> ProjeRolAnketSorusuGuncelle(List<ProjeRolAnketSorusu> anketSorusuListe);
        Task<List<ProjeRolAnketSorusu>> ProjeRolAnketSorusuListeGetir(List<string> idList);
        Task<List<ProjeRolAnketSorusu>> ProjeRolAnketSorusuListeGetir(string projeRolId);
        Task<List<RolAgirlikTipi>> RolAgirlikTipiAktifListeGetir();
    }
}