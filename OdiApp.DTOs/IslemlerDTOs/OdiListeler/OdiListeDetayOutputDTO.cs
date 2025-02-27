using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO;

namespace OdiApp.DTOs.IslemlerDTOs.OdiListeler
{
    public class OdiListeDetayOutputDTO
    {
        public string OdiListeDetayId { get; set; }
        public DateTime ListeyeEklenmeTarihi { get; set; }
        public OdiTalepOutputDTO OdiTalep { get; set; }
        public PerformerOdiOutputDTO PerformerOdi { get; set; }
    }
}
