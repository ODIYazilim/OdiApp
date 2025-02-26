using OdiApp.DTOs.SharedDTOs.PerformerDTOs.DeneyimDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.EgitimDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.FizikselOzelliklerDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.PerformerEtiketDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTipiDTOs;
using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.PerformerDTOs
{
    public class RolOzellikAyarlariDTO
    {
        public List<FizikselOzellikTipiOutputDTO> FizikselOzellikListesi { get; set; }
        public List<DeneyimDTO> DeneyimListesi { get; set; }
        public List<EgitimTipiDTO> EgitimListesi { get; set; }
        public List<YetenekTipiDTO> YetenekListesi { get; set; }
        public List<PerformerEtiketTipiDTO> PerformerEtiketListesi { get; set; }
    }
}