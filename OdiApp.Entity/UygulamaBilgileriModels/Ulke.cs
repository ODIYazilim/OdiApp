using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.UygulamaBilgileriModels
{
    public class Ulke : BaseModel
    {
        public string Iso { get; set; }

        public string Name { get; set; }

        public string Nicename { get; set; }

        public string? Iso3 { get; set; }

        public short? Numcode { get; set; }

        public int Phonecode { get; set; }
    }
}