using OdiApp.DTOs.SharedDTOs.PerformerDTOs.FizikselOzelliklerDTOs;
using OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;

namespace OdiApp.DataAccessLayer.PerformerDataServices.FizikselOzellikler;

public interface IFizikselOzellikDataService
{
    Task<FizikselOzellikTipi> FizikselOzellikTipiEkle(FizikselOzellikTipi tip);
    Task<FizikselOzellikTipi> FizikselOzellikTipiGuncelle(FizikselOzellikTipi tip);
    Task<FizikselOzellikTipi> FizikselOzellikTipiGetir(int tipId);
    Task<bool> FizikselOzellikTipiSil(FizikselOzellikTipi tip);
    Task<List<FizikselOzellikTipi>> FizikselOzellikTipiListe(int dilId);
    Task<List<FizikselOzellikTipiOutputDTO>> FizikselOzellikTipiListesi(int dilId);
    Task<List<FizikselOzellikTipiOutputDTO>> FizikselOzellikTipiListesiByKayitTuruKodu(string kayitTuruKodlari, int dilId);
    Task<List<FizikselOzellik>> FizikselOzellikListesi(string fizikselOzellikTipKodu);
    Task<List<FizikselOzellik>> FizikselOzellikListesiByDilId(string fizikselOzellikTipKodu, int dilId);
    Task<List<FizikselOzellik>> TumFizikselOzellikListesiByDilId(int dilId);
}