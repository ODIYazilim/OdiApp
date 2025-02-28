using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    public class SosyalMedya : BaseModel
    {
        public string SMAdi { get; set; }
        public int Sira { get; set; }
        public bool Aktif { get; set; }
        public string Icon { get; set; }
    }
}