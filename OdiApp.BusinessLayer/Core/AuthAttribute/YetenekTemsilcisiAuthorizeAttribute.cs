using Microsoft.AspNetCore.Mvc.Filters;
using OdiApp.BusinessLayer.Core.Exceptions;
using OdiApp.DTOs.Enums;

namespace OdiApp.BusinessLayer.Core.AuthAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class YetenekTemsilcisiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //giriş Yapılmamışsa
            if (!context.HttpContext.User.Identity.IsAuthenticated) throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");


            //giriş Yapılmamışsa

            string kayitGrubu = context.HttpContext.User.FindFirst("KayitGrubuKodu").Value;
            if (kayitGrubu == KayitGrupKodlari.YetenekTemsilcisi || kayitGrubu == KayitGrupKodlari.OdiYoneticisi) return;
            else throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");
        }
    }
}