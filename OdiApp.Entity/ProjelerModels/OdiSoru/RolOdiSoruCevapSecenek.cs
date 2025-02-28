using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiSoru
{
    [Table("RolOdiSoruCevapSecenekleri")]
    public class RolOdiSoruCevapSecenek : StringBaseModel
    {
        public string RolOdiSoruId { get; set; }
        public string Cevap { get; set; }
        public bool DigerMi { get; set; }
        public string DigerAciklama { get; set; }
        public int Sira { get; set; }
    }
}