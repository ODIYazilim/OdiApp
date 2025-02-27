using Microsoft.AspNetCore.Mvc.Filters;
using OdiApp.BusinessLayer.Core.Exceptions;

namespace OdiApp.BusinessLayer.Core.AuthAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //giriş Yapılmamışsa
            if (!context.HttpContext.User.Identity.IsAuthenticated) throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");


            //giriş Yapılmamışsa
            //string kayitGrubu = context.HttpContext.User.FindFirst("KayitGrubu").Value;
            //if (kayitGrubu == KayitGrupKodlari.YetenekTemsilcisi || kayitTuru == KayitTurKodlari.Oyuncu || kayitTuru == (int)KayitTurKodlari.OdiYonetici) return;
            //else throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");
        }
    }
}