using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.PerformerDTOs.PerformerEtiketDTOs
{
    public class PerformerEtiketTipiDTO
    {
        public string PerformerEtiketTipAdi { get; set; }
        public string PerformerEtiketTipKodu { get; set; }
        public List<PerformerEtiketOutputDTO> Liste { get; set; }
    }
}