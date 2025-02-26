using OdiApp.DTOs.PerformerDTOs.ProfilOnayDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.EntityLayer.PerformerModels.ProfilOnayModels;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices;

public interface IProfilOnayLogicService
{
    //Admin
    Task<OdiResponse<NoContent>> ProfilOnayRedNedeniTanimiEkle(ProfilOnayRedNedeniTanimi tip, OdiUser user);
    Task<OdiResponse<NoContent>> ProfilOnayRedNedeniTanimiGuncelle(ProfilOnayRedNedeniTanimi tip, OdiUser user);
    Task<OdiResponse<List<ProfilOnayRedNedeniTanimiDTO>>> ProfilOnayRedNedeniTanimiListe();
    Task<OdiResponse<NoContent>> ProfilOnayRedNedeniTanimiSil(ProfilOnayRedNedeniTanimiIdDTO id);
    Task<OdiResponse<NoContent>> ProfilOnayRedNedeniTanimiDurumDegistir(ProfilOnayRedNedeniTanimiIdDTO id, OdiUser user);

    //performer
    Task<OdiResponse<ProfilOnayGonderDTO>> ProfilOnayaGonder(ProfilOnayGonderDTO onayDTO, OdiUser user, string jwt);
    Task<OdiResponse<List<ProfilOnayOutputDTO>>> ProfilOnaySureci(PerformerIdDTO id);
    Task<OdiResponse<NoContent>> ProfilOnayDurumSorgula(ProfilOnayIdDTO id);
    Task<OdiResponse<ProfilOnayOutputDTO>> ProfilOnaySonDurumGetir(PerformerIdDTO id);

    //Menajer
    Task<OdiResponse<NoContent>> ProfilOnayOnayla(ProfilOnayIdDTO onayId, OdiUser user, string jwt);
    Task<OdiResponse<NoContent>> ProfilOnayGeriAl(ProfilOnayIdDTO onayId, OdiUser user, string jwt);
    Task<OdiResponse<NoContent>> ProfilOnayRed(ProfilOnayRedInputDTO red, OdiUser user, string jwt);
    Task<OdiResponse<NoContent>> ProfilOnayIncele(ProfilOnayIdDTO onayId, OdiUser user, string jwt);

    //Task<OdiResponse<List<ProfilOnayOutputDTO>>> TalepListesi(TalepListesiInputDTO dto, string jwt);
    //Task<OdiResponse<TalepSayisiOutputDTO>> TalepSayisi(MenajerIdDTO dto, string jwt);
}