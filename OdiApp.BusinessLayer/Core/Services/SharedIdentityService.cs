using Microsoft.AspNetCore.Http;
using Odi.Shared.Services.Interface;
using OdiApp.DTOs.SharedDTOs;

namespace Odi.Shared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
        public string GetAdSoyad => _httpContextAccessor.HttpContext.User.FindFirst("AdSoyad").Value;
        public string KayitTuruKodu => _httpContextAccessor.HttpContext.User.FindFirst("KayitTuruKodu").Value;
        public string KayitGrubuKodu => _httpContextAccessor.HttpContext.User.FindFirst("KayitGrubuKodu").Value;
        public OdiUser GetUser => new OdiUser { Id = GetUserId, AdSoyad = GetAdSoyad, KayitTuruKodlari = KayitTuruKodu.Split(',').ToList() };
    }
}