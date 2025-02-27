using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma
{
    public class ProjeMesajDetayListesiInputDTO : PagedDataRequestModel
    {
        public string Kullanici1Id { get; set; }
        public string Kullanici2Id { get; set; }
    }
}