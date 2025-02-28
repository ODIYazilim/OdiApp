using OdiApp.DTOs.ProjelerDTOs.PerformerProje;
using OdiApp.DTOs.ProjelerDTOs.ProjeBilgileriDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;

namespace OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeBilgileri
{
    public interface IProjeLogicService
    {
        Task<OdiResponse<ProjeAyarlariDTO>> ProjeAyarlariGetir(int dilId, string jwtToken);
        Task<OdiResponse<ProjeOutputDTO>> ProjeGetir(ProjeIdDTO proje);

        Task<OdiResponse<ProjeOutputDTO>> YeniProje(ProjeCreateDTO proje, OdiUser user, int dil, string jwtToken);
        Task<OdiResponse<ProjeOutputDTO>> ProjeGuncelle(ProjeUpdateDTO proje, OdiUser user, string jwtToken);
        Task<OdiResponse<bool>> ProjeSil(ProjeIdDTO id);

        Task<OdiResponse<string>> ProjeFotografiGuncelle(ProjeFotografiUpdateDTO foto);
        Task<OdiResponse<List<string>>> ProjeFotografArama(ProjeFotografAramaDto fotoAdi);

        //
        Task<OdiResponse<List<ProjeOutputDTO>>> TumAcikProjeler(int dilId);
        Task<OdiResponse<List<PerformerProjeDTO>>> PerformerProjeListesi(PerformerIdDTO id, string token, int dilId);
        //
        Task<OdiResponse<PerformerProjeDTO>> MenajerProjeOdiDetayGetir(MenajerIslemInputDTO input, string token, int dilId);
        //
        Task<OdiResponse<bool>> ProjeKayitAyarlari(ProjeKayitAyarlariDTO model, OdiUser user);
        Task<OdiResponse<bool>> ProjeYayinaAl(ProjeIdDTO model, OdiUser user);
        Task<OdiResponse<bool>> ProjeYayiniDurdurma(ProjeIdDTO model, OdiUser user);
    }
}