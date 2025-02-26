using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs
{
    public class OdiUser
    {
        public string Id { get; set; }
        public string AdSoyad { get; set; }
        public List<string> KayitTuruKodlari { get; set; }
        public string KayitGrubuKodu { get; set; }
    }
}