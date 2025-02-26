using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTipiDTOs
{
    public class YetenekTipiDTO
    {
        public string YetenekTipiAdi { get; set; }
        public string YetenekTipiKodu { get; set; }
        public List<YetenekOutputDTO> Liste { get; set; }
    }
}