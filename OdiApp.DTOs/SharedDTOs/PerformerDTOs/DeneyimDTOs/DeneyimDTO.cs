using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs.PerformerDTOs.DeneyimDTOs
{
    public class DeneyimDTO
    {
        public string DeneyimKodu { get; set; }
        public string DeneyimAdi { get; set; }
        public List<DeneyimFormAlanlariDTO> Alanlar { get; set; }
    }
}