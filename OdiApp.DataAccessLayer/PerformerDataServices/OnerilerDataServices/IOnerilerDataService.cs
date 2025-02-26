using OdiApp.DTOs.PerformerDTOs.PerformerFiltre;
using OdiApp.EntityLayer.PerformerModels.OnerilerModels;

namespace OdiApp.DataAccessLayer.PerformerDataServices.OnerilerDataServices;

public interface IOnerilerDataService
{
    Task<OneriTalepleri> YeniOneriTalep(OneriTalepleri model);
    Task<List<MenajerPerformerOnerileri>> YeniMenajerPerformerOneri(List<MenajerPerformerOnerileri> model);
    Task<List<PerformerDisplayInfoDTO>> MenajerPerformerOneriListesiGetir(string projeId, string menajerId);
}