using OdiApp.DTOs.PerformerDTOs.PerformerEtiketleriDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerEtiketleriLogicServices;

public interface IPerformerEtiketleriLogicService
{
    #region  Yetenek Temsilcisi Performer Etiket Tipi (Admin)

    Task<OdiResponse<YetenekTemsilcisiPerformerEtiketTipi>> YetenekTemsilcisiPerformerEtiketTipiEkle(YetenekTemsilcisiPerformerEtiketTipi model, OdiUser user);
    Task<OdiResponse<YetenekTemsilcisiPerformerEtiketTipi>> YetenekTemsilcisiPerformerEtiketTipiGuncelle(YetenekTemsilcisiPerformerEtiketTipi model, OdiUser user);
    Task<OdiResponse<NoContent>> YetenekTemsilcisiPerformerEtiketTipiSil(YetenekTemsilcisiPerformerEtiketTipiIdDTO model);
    Task<OdiResponse<List<YetenekTemsilcisiPerformerEtiketTipi>>> YetenekTemsilcisiPerformerEtiketTipiListesiGetir(int dilId);

    #endregion

    #region Performer Etiket (Admin)

    Task<OdiResponse<PerformerEtiket>> PerformerEtiketEkle(PerformerEtiket model, OdiUser user);
    Task<OdiResponse<PerformerEtiket>> PerformerEtiketGuncelle(PerformerEtiket model, OdiUser user);
    Task<OdiResponse<NoContent>> PerformerEtiketSil(PerformerEtiketIdDTO model);
    Task<OdiResponse<List<PerformerEtiket>>> PerformerEtiketListesiGetir(int dilId);

    #endregion

    #region Yetenek Temsilcisi Performer Etiketi (Yetenek Temsilcisi)

    Task<OdiResponse<NoContent>> YetenekTemsilcisiPerformerEtiketiGuncelle(YetenekTemsilcisiPerformerEtiketiUpdateDTO model, OdiUser user);
    Task<OdiResponse<List<YetenekTemsilcisiPerformerEtiketiListesiOutputDTO>>> YetenekTemsilcisiPerformerEtiketiListesiGetir(YetenekTemsilcisiPerformerEtiketiListesiDTO? model, int dilId);

    #endregion
}