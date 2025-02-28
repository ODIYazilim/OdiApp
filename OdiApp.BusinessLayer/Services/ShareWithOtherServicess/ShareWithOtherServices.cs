using OdiApp.DataAccessLayer.ProjelerDataServices.ProjeBilgileri;
using OdiApp.DTOs.ProjelerDTOs.ShareWithOtherServices;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.ProjeDTOs.ProjeBilgileriDTOs;
using OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri;

namespace OdiApp.BusinessLayer.Services.ShareWithOtherServicess
{
    public class ShareWithOtherServices : IShareWithOtherServices
    {
        IProjeDataService _projeDataService;
        public ShareWithOtherServices(IProjeDataService projeDataService)
        {
            _projeDataService = projeDataService;
        }

        public async Task<OdiResponse<List<ProjeYetkiliBilgisiDTO>>> ProjeYetkilileriGetir(ProjeIdDTO projeId)
        {
            List<ProjeYetkili> list = await _projeDataService.ProjeYetkiliListesi(projeId.ToString());
            List<ProjeYetkiliBilgisiDTO> yetkiliListesi = list.Select(x => new ProjeYetkiliBilgisiDTO { YetkiliId = x.YetkiliId }).ToList();
            return OdiResponse<List<ProjeYetkiliBilgisiDTO>>.Success("Yetkili Listesi Getirildi", yetkiliListesi, 200);
        }
    }
}