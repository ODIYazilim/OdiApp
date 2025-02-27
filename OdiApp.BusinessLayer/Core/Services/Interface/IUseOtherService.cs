using System.Threading.Tasks;

namespace OdiApp.BusinessLayer.Core.Services.Interface
{
    public interface IUseOtherService
    {
        Task<dynamic> PostMethod(object obj, string endpoint, string jwtToken);
        Task<dynamic> OdiPostMethod(object obj, string endpoint, string jwtToken);
        Task<dynamic> GetMethod(string endpoint, string jwtToken);
    }
}
