using OdiApp.DTOs.PerformerDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.BusinessLayer.Services.PerformerLogicServices;

public interface IProfilLogicService
{
    Task<OdiResponse<ProfilDTO>> PerformerProfiliGetir(PerformerIdDTO id, string jwtToken);
}