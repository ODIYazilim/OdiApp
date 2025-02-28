using OdiApp.EntityLayer.Base;

namespace OdiApp.EntityLayer.ProjelerModels.ProjeRolBilgisi
{
    public class RolAgirlikTipi : BaseModel
    {
        public int DilId { get; set; }
        public string RolAgirligi { get; set; }
        public string RolAgirlikKodu { get; set; }
        public bool Aktif { get; set; }
    }
}