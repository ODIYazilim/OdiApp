using OdiApp.DTOs.IslemlerDTOs.PerformerIslemler;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.BusinessLayer.Services.IslemlerLogicServices.PerformerIslemler
{
    public interface IPerforlerIslemlerLogicService
    {
        Task<OdiResponse<List<PerformerIslemDTO>>> PerformerIslemListesi(PerformerIdDTO performerId);
        Task<OdiResponse<List<PerformerIslemDTO>>> MenajerProjeIslem(MenajerIslemInputDTO input);
    }
}
