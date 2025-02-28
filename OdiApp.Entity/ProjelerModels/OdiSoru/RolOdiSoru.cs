using OdiApp.EntityLayer.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdiApp.EntityLayer.ProjelerModels.OdiSoru
{
    [Table("RolOdiSorular")]
    public class RolOdiSoru : StringBaseModel
    {
        public string ProjeRolOdiId { get; set; }
        public string Soru { get; set; }
        public bool CokluSecimSorusu { get; set; }
        public bool CokluCevapIzni { get; set; }
        public List<RolOdiSoruCevapSecenek> CevapSecenekleri { get; set; }
    }
}