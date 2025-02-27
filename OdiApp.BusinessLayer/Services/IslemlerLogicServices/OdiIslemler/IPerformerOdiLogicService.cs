using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.IslemlerModels.OdiIslemler;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.OdiIslemler
{
    public interface IPerformerOdiLogicService
    {
        Task<OdiResponse<PerformerOdiOutputDTO>> YeniPerformerOdi(PerformerOdiCreateDTO performerOdi, OdiUser user);



        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiGuncelle(PerformerOdiUpdateDTO performerOdi, OdiUser user);
        Task<OdiResponse<bool>> MenajerOnayinaGonder(string performerOdiId);
        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiDetayGetir(OdiTalepIdDTO talepId);
        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerInceledi(PerformerOdiIdDTO talepId);
        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerAktifPasif(PerformerOdiIdDTO talepId);
        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerGizle(PerformerOdiIdDTO talepId);
        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerOnayi(PerformerOdiIdDTO talepId);
        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiMenajerTekrarCekTalebi(PerformerOdiMenajerTekrarCekInputDTO tekrarCek);
        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiYapimInceledi(PerformerOdiIdDTO talepId);
        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiYapimAktifPasif(PerformerOdiIdDTO talepId);
        Task<OdiResponse<PerformerOdiOutputDTO>> PerformerOdiYapimGizle(PerformerOdiIdDTO talepId);
        PerformerOdiOutputDTO PerformerOdiOuputGetir(PerformerOdi odi);
        Task<PerformerOdi> PerformerOdiGetir(string odiTalepId);
        Task<OdiTalepOutputDTO> OdiTalepGetir(string odiTalepId);//odiliste logic te odi islem data servis i constracter a eklenmiyor. Çünkü Performer logic te ekli. Bu neden odi talep i bu logic servisten getirmek gerekiyor.
    }
}
