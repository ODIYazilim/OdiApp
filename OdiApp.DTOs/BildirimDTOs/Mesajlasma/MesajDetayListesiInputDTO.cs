using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.DTOs.BildirimDTOs.Mesajlasma
{
    public class MesajDetayListesiInputDTO : PagedDataRequestModel
    {
        public string Kullanici1Id { get; set; }
        public string Kullanici2Id { get; set; }
    }
}