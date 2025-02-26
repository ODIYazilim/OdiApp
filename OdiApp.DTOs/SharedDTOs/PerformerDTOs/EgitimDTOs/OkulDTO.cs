using System.Collections.Generic;
namespace OdiApp.DTOs.SharedDTOs.PerformerDTOs.EgitimDTOs
{
    public class OkulDTO
    {
        public int OkulId { get; set; }
        public string OkulAdi { get; set; }
        public List<OkulBolumDTO> Bolumler { get; set; }
    }
}