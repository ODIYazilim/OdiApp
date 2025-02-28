using OdiApp.DTOs.SharedDTOs.UygulamaBilgileriDTOs;

namespace OdiApp.DTOs.ProjelerDTOs.ProjeRolBilgileri.ProjeRolDTO
{
    public class RolBilgisiAyarlariOutputDTO
    {
        public List<RolTuruDTO> KayitTurlariListesi { get; set; }
        public List<RolAgirlikTipiDTO> RolAgirlikTipleriListesi { get; set; }
    }
}