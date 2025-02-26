using System;

namespace OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs
{
    public class PerformerYetenekTemsilcisiAtamaDTO
    {
        public string PerformerId { get; set; }
        public string MenajerId { get; set; }
        public bool Aktif { get; set; }
        public DateTime AtamaTarihi { get; set; }
        public DateTime? AtamaIptalTarihi { get; set; }
    }
}