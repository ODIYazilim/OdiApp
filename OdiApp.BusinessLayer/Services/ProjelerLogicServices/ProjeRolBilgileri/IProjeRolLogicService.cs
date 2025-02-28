using OdiApp.DTOs.IslemlerDTOs;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolAnketSorusuDTO;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolDTO;
using OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolOzellikDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.CVDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeRolPerformerDTOs;

namespace OdiApp.BusinessLayer.Services.ProjelerLogicServices.ProjeRolBilgileri
{
    public interface IProjeRolLogicService
    {
        Task<OdiResponse<FiltrelenenPerformerlarInputDTO>> ProjeRolFiltreGetir(ProjeRolIdDTO projeRolIdDTO);
        Task<OdiResponse<ProjeRolOzellikOutputDTO>> ProjeRolOzellikGetir(ProjeRolIdDTO projeRolIdDTO);
        Task<OdiResponse<bool>> ProjeRolOzellikSil(ProjeRolIdDTO projeRolIdDTO);
        Task<OdiResponse<bool>> YeniProjeRolOzellik(ProjeRolOzellikCreateDTO ozellikDTO, OdiUser user);
        Task<OdiResponse<bool>> ProjeRolOzellikGuncelle(ProjeRolOzellikUpdateDTO ozellikDTO, OdiUser user);
        Task<OdiResponse<RolOzellikAyarlariDTO>> RolOzellikAyarlari(KayitTuruKodlariDTO kayitTuruKodlariDTO, string jwt);
        Task<OdiResponse<RolBilgisiAyarlariOutputDTO>> RolBilgisiAyarlari(string jwt);
        Task<OdiResponse<ProjeRolOutputDTO>> YeniProjeRol(ProjeRolCreateDTO rol, OdiUser user);
        Task<OdiResponse<bool>> ProjeRolKopyala(ProjeRolIdDTO model, OdiUser user);
        Task<OdiResponse<ProjeRolOutputDTO>> ProjeRolGuncelle(ProjeRolUpdateDTO rol, OdiUser user);
        Task<List<ProjeRolOutputDTO>> ProjeRolleriGetir(string projeId);
        Task<OdiResponse<List<ProjeRolOutputDTO>>> ProjeRolleriGetir(ProjeIdDTO id);
        Task<ProjeRolOutputDTO> ProjeRolGetir(string projeRolId);
        Task<OdiResponse<AlternatifButceOutputDTO>> AlternatifButceGetir(ProjeRolIdDTO projeRolIdDTO);
        Task<OdiResponse<bool>> AlternatifButceGuncelle(AlternatifButceUpdateDTO model, OdiUser user);
        Task<OdiResponse<ProjeRolOutputDTO>> ProjeRolGetir(ProjeRolIdDTO id);
        Task<OdiResponse<List<ProjeRolPerformerOutputDTO>>> YeniProjeRolPerformer(ProjeRolPerformerCreateDTO model, OdiUser user);
        Task<OdiResponse<List<ProjeRolPerformerOutputDTO>>> ProjeRolPerformerListele(ProjeIdDTO model);
        Task<OdiResponse<bool>> YeniProjeRolAnketSorusu(List<ProjeRolAnketSorusuCreateDTO> anketSorusuCreateDTOList, OdiUser user);
        Task<OdiResponse<bool>> ProjeRolAnketSorusuGuncelle(List<ProjeRolAnketSorusuUpdateDTO> anketSorusuUpdateDTOList, OdiUser user);
        Task<OdiResponse<bool>> ProjeRolAnketSorusuSil(List<ProjeRolAnketSorusuIdDTO> anketSorusuIdDTOList);
        Task<OdiResponse<List<ProjeRolAnketSorusuOutputDTO>>> ProjeRolAnketSorusuListeGetir(ProjeRolIdDTO model);
        Task<OdiResponse<List<ProjeRolOpsiyonDetayOutputDTO>>> ProjeRolOpsiyonDetay(ProjeIdDTO id);
    }
}