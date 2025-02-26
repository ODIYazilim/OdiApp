using OdiApp.EntityLayer.PerformerModels.PerformerEtiketleriModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.PerformerEtiketleriDataServices;

public interface IPerformerEtiketleriDataService
{
    #region Yetenek Temsilcisi Performer Etiket Tipi

    Task<YetenekTemsilcisiPerformerEtiketTipi> YeniYetenekTemsilcisiPerformerEtiketTipi(YetenekTemsilcisiPerformerEtiketTipi model);
    Task<YetenekTemsilcisiPerformerEtiketTipi> YetenekTemsilcisiPerformerEtiketTipiGuncelle(YetenekTemsilcisiPerformerEtiketTipi model);
    Task<List<YetenekTemsilcisiPerformerEtiketTipi>> YetenekTemsilcisiPerformerEtiketTipiListesiGetir(int dilId, bool onlyAktif = true);
    Task<YetenekTemsilcisiPerformerEtiketTipi> YetenekTemsilcisiPerformerEtiketTipiGetirById(string id);
    Task<bool> YetenekTemsilcisiPerformerEtiketTipiSil(YetenekTemsilcisiPerformerEtiketTipi model);

    #endregion

    #region Performer Etiket

    Task<PerformerEtiket> YeniPerformerEtiket(PerformerEtiket model);
    Task<PerformerEtiket> PerformerEtiketGuncelle(PerformerEtiket model);
    Task<List<PerformerEtiket>> PerformerEtiketListesiGetir(int dilId, bool onlyAktif = true);
    Task<PerformerEtiket> PerformerEtiketGetirById(string id);
    Task<bool> PerformerEtiketSil(PerformerEtiket model);

    #endregion

    #region Yetenek Temsilcisi Performer Etiketleri

    Task<List<YetenekTemsilcisiPerformerEtiketi>> YetenekTemsilcisiPerformerEtiketiListesiGetir(string yetenekTemsilcisiId, string performerId);
    Task<YetenekTemsilcisiPerformerEtiketi> YetenekTemsilcisiPerformerEtiketiGuncelle(YetenekTemsilcisiPerformerEtiketi model);
    Task<YetenekTemsilcisiPerformerEtiketi> YetenekTemsilcisiPerformerEtiketiEkle(YetenekTemsilcisiPerformerEtiketi model);
    Task<bool> YetenekTemsilcisiPerformerEtiketiTopluEkle(List<YetenekTemsilcisiPerformerEtiketi> model);
    Task<bool> YetenekTemsilcisiPerformerEtiketiSil(string id);
    Task<bool> YetenekTemsilcisiPerformerEtiketiTopluSil(List<YetenekTemsilcisiPerformerEtiketi> removeList);

    #endregion
}