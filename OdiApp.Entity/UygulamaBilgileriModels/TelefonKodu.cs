using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    [Table("TelefonKodlari")]
    public class TelefonKodu : BaseModel
    {
        public int Kod { get; set; }
        public bool Aktif { get; set; } = true;
        public string Ulke { get; set; }
        public string UlkeKisaltma { get; set; }
        public string BayrakLink { get; set; }
    }
}