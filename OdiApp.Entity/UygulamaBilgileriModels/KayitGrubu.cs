using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    [Table("KayitGruplari")]
    public class KayitGrubu : BaseModel
    {
        public int DilId { get; set; }
        public string GrupKodu { get; set; }
        public string GrupAdi { get; set; }
        public int Sira { get; set; }
        public bool Aktif { get; set; }
    }
}
