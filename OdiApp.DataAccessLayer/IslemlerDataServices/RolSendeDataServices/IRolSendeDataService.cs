using OdiApp.EntityLayer.IslemlerModels.RolSendeModels;

namespace OdiApp.DataAccessLayer.IslemlerDataServices.RolSendeDataServices
{
    public interface IRolSendeDataService
    {
        Task<RolSende> YeniRolSende(RolSende model);
    }
}