using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.ProjelerDTOs.OdiFotograf;
using OdiApp.DTOs.ProjelerDTOs.OdiSes;
using OdiApp.DTOs.ProjelerDTOs.OdiSoru;
using OdiApp.DTOs.ProjelerDTOs.OdiVideo;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolOdiBilgisi;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolOdiBilgileri
{
    public interface IProjeRolOdiLogicService
    {
        //rol odi
        Task<OdiResponse<ProjeRolOdiOutputDTO>> RolOdiBilgileriniGetir(ProjeRolIdDTO projeRolId);
        Task<OdiResponse<ProjeRolOdiOutputDTO>> YeniProjeRolOdi(ProjeRolOdiCreateDTO odiDTO, OdiUser user);
        Task<OdiResponse<ProjeRolOdiOutputDTO>> ProjeRolOdiGuncelle(ProjeRolOdiUpdateDTO odiDTO, OdiUser user);
        Task<OdiResponse<ProjeRolOdiOutputDTO>> ProjeRolOdiGetir(ProjeRolIdDTO id);
        Task<ProjeRolOdiOutputDTO> ProjeRolOdiGetir(string projeRolId);

        //rol odi foto
        Task<OdiResponse<RolOdiFotoDTO>> YeniRolOdiFoto(RolOdiFotoCreateDTO odiFoto, OdiUser user);
        Task<OdiResponse<RolOdiFotoDTO>> RolOdiFotoPozGuncelle(RolOdiFotoPozUpdateListDTO updateDTO, OdiUser user);

        Task<OdiResponse<RolOdiFotoDTO>> RolOdiFotoYonetmenNotGuncelle(RolOdiFotoYonetmenNotuUpdateDTO not, OdiUser user);
        Task<OdiResponse<RolOdiFotoDTO>> RolOdiFotoOrnekFotoListesiGuncelle(RolOdiFotoOrnekFotografUpdateListDTO updateDTO, OdiUser user);
        Task<RolOdiFotoDTO> RolOdiFotoGetir(string rolOdiId);

        //rol Odi Ses
        Task<RolOdiSesDTO> RolOdiSesGetir(string rolOdiId);
        Task<OdiResponse<RolOdiSesDTO>> YeniRolOdiSes(RolOdiSesmatikCreateDTO ses, OdiUser user);
        Task<OdiResponse<RolOdiSesDTO>> RolOdiSesListesiGuncelle(RolOdiSesUpdateListDTO sesListDTO, OdiUser user);

        Task<OdiResponse<RolOdiSesDTO>> RolOdiSesSenaryoGuncelle(RolOdiSesSenaryoUpdateDTO senaryo, OdiUser user);
        Task<OdiResponse<RolOdiSesDTO>> RolOdiSesYonetmenNotuGuncelle(RolOdiSesYonetmenNotuUpdateDTO not, OdiUser user);

        //Rol Odi Video
        Task<RolOdiVideomatikDTO> RolOdiVideoGetir(string rolOdiId);
        Task<OdiResponse<RolOdiVideomatikDTO>> YeniRolOdiVideomatik(RolOdiVideomatikCreateDTO video, OdiUser user);
        Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoGuncelle(RolOdiVideoUpdateDTO video, OdiUser user);
        Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoDetayGuncelle(RolOdiVideoDetayUpdateListDTO detayUpdate, OdiUser user);
        Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoOrnekOyunGuncelle(RolOdiVideoOrnekOyunUpdateDTO ideoOrnekOyun, OdiUser user);
        Task<OdiResponse<RolOdiVideomatikDTO>> YeniRolOdiVideoOrnek(RolOdiVideoOrnekOyunCreateDTO ornekOyun, OdiUser user);
        Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoOrnekOyunSil(RolOdiVideoOrnekOyunIdDTO id);
        Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoSenaryoGuncelle(RolOdiVideoSenaryoUpdateDTO senaryo, OdiUser user);
        Task<OdiResponse<RolOdiVideomatikDTO>> RolOdiVideoYonetmenNotuGuncelle(RolOdiVideoYonetmenNotuUpdateDTO not, OdiUser user);

        //Rol Odi Sorumatik
        Task<RolOdiSorumatikDTO> RolOdiSoruGetir(string rolOdiId);
        Task<OdiResponse<RolOdiSorumatikDTO>> YeniRolOdiSorumatik(RolOdiSorumatikCreateDTO sorumatik, OdiUser user);
        Task<OdiResponse<RolOdiSorumatikDTO>> RolOdiSoruGuncelle(RolOdiSoruUpdateDTO sorumatik, OdiUser user);
        Task<OdiResponse<RolOdiSorumatikDTO>> RolOdiSoruSil(RolOdiSoruIdDTO id);
        Task<OdiResponse<RolOdiSorumatikDTO>> RolOdiSoruAciklamaGuncelle(RolOdiSoruAciklamaUpdateDTO aciklama, OdiUser user);
    }
}