using System;

namespace OdiApp.DTOs.SharedDTOs.GlobalDTOs.YetenekTemsilcisiAbonelikDTOs
{
    public class YetenekTemsilcisiAbonelikBilgileriGetirOutputDTO
    {
        public int OdemePeriodu { get; set; }
        public DateTime AbonelikBaslangicTarihi { get; set; }
        public DateTime AbonelikBitisTarihi { get; set; }
    }
}