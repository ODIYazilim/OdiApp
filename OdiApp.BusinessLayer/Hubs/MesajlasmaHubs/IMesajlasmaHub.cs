using OdiApp.DTOs.BildirimDTOs.Mesajlasma;
using OdiApp.DTOs.BildirimDTOs.ProjeMesajlasma;

namespace OdiApp.BusinessLayer.Hubs.MesajlasmaHubs
{
    public interface IMesajlasmaHub
    {
        Task YeniMesajDinle(MesajDetayOutputDTO mesajDetayOutput);
        Task MesajOkunduDinleme(List<MesajOkunduDinlemeOutputDTO> mesajOkunduDinlemeOutputDTOList);
        Task YeniProjeMesajDinle(ProjeMesajDetayOutputDTO projeMesajDetayOutput);
        Task ProjeMesajOkunduDinleme(List<ProjeMesajOkunduDinlemeOutputDTO> projeMesajOkunduDinlemeOutputDTOList);
    }
}