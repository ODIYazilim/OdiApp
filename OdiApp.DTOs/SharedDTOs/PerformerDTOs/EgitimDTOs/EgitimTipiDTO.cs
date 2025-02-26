using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.PerformerDTOs.EgitimDTOs
{
    public class EgitimTipiDTO
    {
        public int EgitimTipiId { get; set; }
        public string EgitimTipi { get; set; }
        public List<OkulDTO> Okullar { get; set; }
    }
}