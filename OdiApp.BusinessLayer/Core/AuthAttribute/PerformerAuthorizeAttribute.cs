using Microsoft.AspNetCore.Mvc.Filters;
using OdiApp.BusinessLayer.Core.Exceptions;
using OdiApp.DTOs.Enums;

namespace Odi.Shared.AuthAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PerformerAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //giriş Yapılmamışsa
            if (!context.HttpContext.User.Identity.IsAuthenticated) throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");


            //giriş Yapılmamışsa
            string kayitGrubu = context.HttpContext.User.FindFirst("KayitGrubuKodu").Value;
            if (kayitGrubu == KayitGrupKodlari.Yetenek || kayitGrubu == KayitGrupKodlari.OdiYoneticisi) return;
            else throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");
        }
    }
}