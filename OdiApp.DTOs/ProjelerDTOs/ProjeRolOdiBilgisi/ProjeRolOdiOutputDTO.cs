using OdiApp.DTOs.ProjelerDTOs.OdiFotograf;
using OdiApp.DTOs.ProjelerDTOs.OdiSes;
using OdiApp.DTOs.ProjelerDTOs.OdiSoru;
using OdiApp.DTOs.ProjelerDTOs.OdiVideo;

namespace OdiApp.DTOs.ProjelerDTOs.ProjeRolOdiBilgisi
{
    public class ProjeRolOdiOutputDTO
    {
        public string ProjeRolOdiId { get; set; }
        public string ProjeRolId { get; set; }
        public string SonOdilemeTarihi { get; set; }
        public string SonOdilemeSaati { get; set; }

        public DateTime SonOdilemeTarihi2 { get; set; }

        public RolOdiFotoDTO Fotomatik { get; set; }
        public RolOdiSesDTO Sesmatik { get; set; }
        public RolOdiVideomatikDTO Videomatik { get; set; }
        public RolOdiSorumatikDTO Sorumatik { get; set; }
    }
}