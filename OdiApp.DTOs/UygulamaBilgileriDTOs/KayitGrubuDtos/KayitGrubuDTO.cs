using OdiApp.DTOs.UygulamaBilgileriDTOs.KayitTuruDtos;

namespace OdiApp.DTOs.UygulamaBilgileriDTOs.KayitGrubuDtos
{
    public class KayitGrubuDTO
    {
        public string GrupKodu { get; set; }
        public string GrupAdi { get; set; }

        public List<KayitTuruDTO> KayitTurleri { get; set; }
    }
}