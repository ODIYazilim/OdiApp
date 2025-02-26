using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.PerformerDTOs.FizikselOzelliklerDTOs
{
    public class FizikselOzellikTipiOutputDTO
    {
        public string FizikselOzellikTipAdi { get; set; }
        public string FizikselOzellikTipKodu { get; set; }
        public List<FizikselOzellikOutputDTO> Liste { get; set; }
    }
}