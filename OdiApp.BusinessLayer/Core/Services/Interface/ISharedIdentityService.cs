using OdiApp.DTOs.SharedDTOs;

namespace Odi.Shared.Services.Interface
{
    public interface ISharedIdentityService
    {
        public OdiUser GetUser { get; }
    }
}
