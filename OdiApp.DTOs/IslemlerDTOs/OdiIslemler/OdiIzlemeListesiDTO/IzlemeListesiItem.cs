using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiTalepDTOs;
using OdiApp.DTOs.IslemlerDTOs.OdiIslemler.PerformerOdiDTO;

namespace OdiApp.DTOs.IslemlerDTOs.OdiIslemler.OdiIzlemeListesiDTO
{
    public class IzlemeListesiItem
    {
        public OdiTalepOutputDTO OdiTalep { get; set; }
        public PerformerOdiOutputDTO PerformerOdi { get; set; }
    }
}
