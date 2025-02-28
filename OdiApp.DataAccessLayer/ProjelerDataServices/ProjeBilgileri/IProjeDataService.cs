using OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri;

namespace OdiApp.DataAccessLayer.ProjelerDataServices.ProjeBilgileri
{
    public interface IProjeDataService
    {
        Task<Proje> YeniProje(Proje proje);
        Task<Proje> ProjeGuncelle(Proje proje);
        Task<Proje> ProjeGetir(string projeId);
        Task<bool> ProjeSil(string projeId);
        Task<List<string>> ProjeFotografiArama(string fotografAdi);

        #region Proje Yetkili 

        Task<List<ProjeYetkili>> ProjeYetkiliListesiGuncelle(List<ProjeYetkili> list);
        Task<ProjeYetkili> ProjeYetkiliGuncelle(ProjeYetkili py);
        Task<List<ProjeYetkili>> ProjeYeniYetkiliListesi(List<ProjeYetkili> list);
        Task ProjeYetkiliListesiSil(List<ProjeYetkili> list);
        Task<ProjeYetkili> ProjeYetkiliGetir(string projeYetkiliId);
        Task<List<ProjeYetkili>> ProjeYetkiliListesi(string projeId);

        #endregion

        #region Proje Türleri

        Task<List<ProjeTuru>> ProjeTuruListe(int dilId);

        #endregion

        #region Proje Listeleri

        Task<List<Proje>> TumProjeler(int dilId);
        Task<List<Proje>> PerformerProjeListesi(List<string> projeIdList, int dilId);

        #endregion

        #region Proje Default Logo

        Task<List<ProjeDefaultLogo>> ProjeDefaultLogoListe();

        #endregion

        #region Proje Katılım Bölgesi

        Task<List<ProjeKatilimBolgesi>> ProjeKatilimBolgesiListe();

        #endregion
    }
}