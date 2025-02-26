using Microsoft.AspNetCore.Mvc.Filters;
using OdiApp.BusinessLayer.Core.Exceptions;
using OdiApp.DTOs.Enums;

namespace Odi.Shared.AuthAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class YapimAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated) throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");


            //giriş Yapılmamışsa
            string kayitGrubu = context.HttpContext.User.FindFirst("KayitGrubuKodu").Value;
            if (kayitGrubu == KayitGrupKodlari.Yapim || kayitGrubu == KayitGrupKodlari.OdiYoneticisi) return;
            else throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");
        }
    }
}