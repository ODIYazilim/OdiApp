using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeBilgileri
{
    [Table("ProjeTurleri")]
    public class ProjeTuru : BaseModel
    {
        public string Tur { get; set; }
        public string ProjeTurKodu { get; set; }
        public int DilId { get; set; }
        public int Sira { get; set; }
    }
}