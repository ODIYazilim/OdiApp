using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.PerformerDTOs.FizikselOzellikler;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.FizikselOzellikler;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.FizikselOzellikler.Interfaces;

public interface IAdminFizikselOzellikLogicService
{
    Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiEkle(FizikselOzellikTipi tip, OdiUser user);
    Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiGuncelle(FizikselOzellikTipi tip, OdiUser user);
    Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiListe(DilIdDTO dilId);
    Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiSil(FizikselOzellikTipiIdDTO id);
    Task<OdiResponse<List<FizikselOzellikTipi>>> FizikselOzellikTipiDurumDegistir(FizikselOzellikTipiIdDTO id, OdiUser user);
}
