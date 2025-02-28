using OdiApp.EntityLayer.ProjelerModels.OdiFotograf;
using OdiApp.EntityLayer.ProjelerModels.OdiSes;
using OdiApp.EntityLayer.ProjelerModels.OdiSoru;
using OdiApp.EntityLayer.ProjelerModels.OdiVideo;
using OdiApp.EntityLayer.ProjelerModels.ProjeRolOdi;

namespace OdiApp.DataAccessLayer.ProjelerDataServices.ProjeRolOdiBilgileri
{
    public interface IProjeRolOdiDataService
    {
        Task<ProjeRolOdi> YeniProjeRolOdi(ProjeRolOdi odi);
        Task<List<ProjeRolOdi>> YeniProjeRolOdi(List<ProjeRolOdi> projeRolOdiListe);
        Task<ProjeRolOdi> ProjeRolOdiGuncelle(ProjeRolOdi odi);
        Task<ProjeRolOdi> ProjeRolOdiGetir(string projeRolId);
        Task<List<ProjeRolOdi>> ProjeRolOdiListeGetir(string projeRolId);

        #region FOTOMATİK
        Task<RolOdiFotoPoz> YeniRolOdiFotoPoz(RolOdiFotoPoz odiFoto);
        Task<List<RolOdiFotoPoz>> RolOdiFotoPozListesiGuncelle(List<RolOdiFotoPoz> list);
        Task<List<RolOdiFotoPoz>> YeniRolOdiFotoPozList(List<RolOdiFotoPoz> odiFotoList);
        Task<bool> RolOdiFotoPozListesiSil(List<RolOdiFotoPoz> odiFotoList);
        Task<List<RolOdiFotoPoz>> RolOdiFotoPozListesiGetir(string rolOdiId);

        Task<RolOdiFotoYonetmenNotu> YeniRolOdiFotoYonetmenNotu(RolOdiFotoYonetmenNotu yonetmenNotu);
        Task<List<RolOdiFotoYonetmenNotu>> YeniRolOdiFotoYonetmenNotu(List<RolOdiFotoYonetmenNotu> yonetmenNotuList);
        Task<RolOdiFotoYonetmenNotu> OdiRolFotoYonetmenNotuGuncelle(RolOdiFotoYonetmenNotu yonetmenNotu);
        Task<List<RolOdiFotoYonetmenNotu>> RolOdiFotoYonetmenNotuListesiGetir(string rolOdiId);
        Task<RolOdiFotoYonetmenNotu> RolOdiFotoYonetmenNotuGetir(string rolOdiId);

        Task<List<RolOdiFotoOrnekFotograf>> YeniRolOdiFotoOrnekFotolar(List<RolOdiFotoOrnekFotograf> ornekFotoListesi);
        Task<List<RolOdiFotoOrnekFotograf>> RolOdiFotoOrnekFotoListeGuncelle(List<RolOdiFotoOrnekFotograf> ornekFotoListesi);
        Task<bool> RolOdiOrnekFotoListesiSil(List<RolOdiFotoOrnekFotograf> ornekFotoListesi);
        Task<List<RolOdiFotoOrnekFotograf>> RolOdiFotoOrnekFotoListesiGetir(string rolOdiId);

        #endregion

        #region SESMATİK

        Task<RolOdiSes> YeniRolOdiSes(RolOdiSes odiSes);
        Task<List<RolOdiSes>> RolOdiSesListesiGuncelle(List<RolOdiSes> list);
        Task<List<RolOdiSes>> YeniRolOdiSesList(List<RolOdiSes> odiSesList);
        Task<bool> RolOdiSesListesiSil(List<RolOdiSes> odiSesList);

        Task<RolOdiSesYonetmenNotu> YeniRolOdiSesYonetmenNotu(RolOdiSesYonetmenNotu yonetmenNotu);
        Task<List<RolOdiSesYonetmenNotu>> YeniRolOdiSesYonetmenNotu(List<RolOdiSesYonetmenNotu> yonetmenNotu);
        Task<RolOdiSesYonetmenNotu> OdiRolSesYonetmenNotuGuncelle(RolOdiSesYonetmenNotu yonetmenNotu);

        Task<RolOdiSesSenaryo> YeniRolOdiSesSenaryo(RolOdiSesSenaryo senaryo);
        Task<List<RolOdiSesSenaryo>> YeniRolOdiSesSenaryo(List<RolOdiSesSenaryo> senaryoList);
        Task<RolOdiSesSenaryo> RolOdiSesSenaryoGuncelle(RolOdiSesSenaryo senaryo);
        Task<bool> RolOdiSesSenaryoSil(string senaryoId);

        Task<List<RolOdiSes>> RolOdiSesListesiGetir(string rolOdiId);
        Task<RolOdiSesYonetmenNotu> RolOdiSesYonetmenNotuGetir(string rolOdiId);
        Task<List<RolOdiSesYonetmenNotu>> RolOdiSesYonetmenNotuListesiGetir(string rolOdiId);
        Task<RolOdiSesSenaryo> RolOdiSesSenaryoGetir(string rolOdiId);
        Task<List<RolOdiSesSenaryo>> RolOdiSesSenaryoListesiGetir(string rolOdiId);
        #endregion

        #region Videomatik

        Task<RolOdiVideo> YeniRolOdiVideo(RolOdiVideo video);
        Task<List<RolOdiVideo>> YeniRolOdiVideo(List<RolOdiVideo> video);
        Task<RolOdiVideo> RolOdiVideoGuncelle(RolOdiVideo video);
        Task<RolOdiVideo> RolOdiVideoGetir(string rolOdiId);
        Task<List<RolOdiVideo>> RolOdiVideoListesiGetir(string rolOdiId);

        Task<List<RolOdiVideoDetay>> YeniRolOdiVideoDetayList(List<RolOdiVideoDetay> detayList);
        Task<List<RolOdiVideoDetay>> RolOdiVideoDetayListGuncelle(List<RolOdiVideoDetay> detayList);
        Task<bool> RolOdiVideoDetayListSil(List<RolOdiVideoDetay> detayList);
        Task<List<RolOdiVideoDetay>> RolOdiVideoDetayListGetir(string videoId);

        Task<List<RolOdiVideoOrnekOyun>> YeniRolOdiVideoOrnekOyunList(List<RolOdiVideoOrnekOyun> oyunList);
        Task<RolOdiVideoOrnekOyun> RolOdiVideoOrnekOyunGuncelle(RolOdiVideoOrnekOyun oyun);
        Task<RolOdiVideoOrnekOyun> YeniRolOdiVideoOrnekOyun(RolOdiVideoOrnekOyun oyun);
        Task<RolOdiVideoOrnekOyun> RolOdiVideoOrnekOyunGetir(string oyunId);
        Task<bool> RolOdiVideoOrnekOyunSil(string oyunId);
        Task<List<RolOdiVideoOrnekOyun>> RolOdiVideoOrnekOyunListGuncelle(List<RolOdiVideoOrnekOyun> oyunList);
        Task<bool> RolOdiVideoOrnekOyunListSil(List<RolOdiVideoOrnekOyun> oyunList);
        Task<List<RolOdiVideoOrnekOyun>> RolOdiVideoOrnekOyunListGetir(string rolOdiId);

        Task<RolOdiVideoSenaryo> YeniRolOdiVideoSenaryo(RolOdiVideoSenaryo senaryo);
        Task<List<RolOdiVideoSenaryo>> YeniRolOdiVideoSenaryo(List<RolOdiVideoSenaryo> senaryo);
        Task<RolOdiVideoSenaryo> RolOdiVideoSenaryoGuncelle(RolOdiVideoSenaryo senaryo);
        Task<RolOdiVideoSenaryo> RolOdiVideoSenaryoGetir(string rolOdiId);
        Task<List<RolOdiVideoSenaryo>> RolOdiVideoSenaryoListesiGetir(string rolOdiId);

        Task<RolOdiVideoYonetmenNotu> YeniRolOdiVideoYonetmenNotu(RolOdiVideoYonetmenNotu not);
        Task<List<RolOdiVideoYonetmenNotu>> YeniRolOdiVideoYonetmenNotu(List<RolOdiVideoYonetmenNotu> notList);
        Task<RolOdiVideoYonetmenNotu> RolOdiVideoYonetmenNotuGuncelle(RolOdiVideoYonetmenNotu not);
        Task<RolOdiVideoYonetmenNotu> RolOdiVideoYonetmenNotuGetir(string rolOdiId);
        Task<List<RolOdiVideoYonetmenNotu>> RolOdiVideoYonetmenNotuListesiGetir(string rolOdiId);

        #endregion

        #region SORUMATIK
        Task<RolOdiSoru> YeniRolOdiSoru(RolOdiSoru soru);
        Task<List<RolOdiSoru>> YeniRolOdiSoruList(List<RolOdiSoru> soruList);
        Task<RolOdiSoru> RolOdiSoruGuncelle(RolOdiSoru soru);
        Task<List<RolOdiSoru>> RolOdiSoruListGuncelle(List<RolOdiSoru> soruList);
        Task<bool> RolOdiSoruSil(string soruId);
        Task<bool> RolOdiSoruListSil(List<RolOdiSoru> soruList);
        Task<RolOdiSoru> RolOdiSoruGetir(string soruId);
        Task<List<RolOdiSoru>> RolOdiSoruListGetir(string rolOdiId);
        Task<List<RolOdiSoruCevapSecenek>> RolOdiSoruCevapListGuncelle(List<RolOdiSoruCevapSecenek> soruList);
        Task<List<RolOdiSoruCevapSecenek>> YeniRolOdiSoruCevapList(List<RolOdiSoruCevapSecenek> soruList);
        Task<List<RolOdiSoruCevapSecenek>> RolOdiSoruCevapListGetir(string soruId);
        Task<bool> RolOdiSoruCevapListSil(List<RolOdiSoruCevapSecenek> soruList);
        Task<RolOdiSoruAciklama> YeniRolOdiSoruAciklama(RolOdiSoruAciklama aciklama);
        Task<List<RolOdiSoruAciklama>> YeniRolOdiSoruAciklama(List<RolOdiSoruAciklama> aciklamaList);
        Task<RolOdiSoruAciklama> RolOdiSoruAciklamaGuncelle(RolOdiSoruAciklama aciklama);
        Task<RolOdiSoruAciklama> RolOdiSoruAciklamaGetir(string rolOdiId);
        Task<List<RolOdiSoruAciklama>> RolOdiSoruAciklamaListesiGetir(string rolOdiId);

        #endregion
    }
}
