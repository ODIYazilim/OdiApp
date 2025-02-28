using OdiApp.DTOs.ProjelerDTOs.ProjeTurleri;
using OdiApp.DTOs.SharedDTOs.IdentityDTOs.FirmaDTOs;
using OdiApp.DTOs.SharedDTOs.IdentityDTOs.UserDTOs;

namespace OdiApp.DTOs.ProjelerDTOs.ProjeBilgileriDTOs
{
    public class ProjeAyarlariDTO
    {
        public List<ProjeTuruOutputDTO> ProjeTurleri { get; set; }
        public List<ProjeDefaultLogoOutputDTO> LogoListesi { get; set; }
        public List<BirlestirilmisFirmaListeOutputDTO> YapimciFirmaListesi { get; set; }
        public List<ProjeKatilimBolgesiOutputDTO> ProjeKatilimBolgeleri { get; set; }
        public List<YapimListItemDTO> YetkiliListesi { get; set; }
    }
}