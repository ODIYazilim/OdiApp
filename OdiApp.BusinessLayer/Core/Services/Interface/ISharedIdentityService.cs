using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Core.Services.Interface
{
    public interface ISharedIdentityService
    {
        public OdiUser GetUser { get; }
    }
}
