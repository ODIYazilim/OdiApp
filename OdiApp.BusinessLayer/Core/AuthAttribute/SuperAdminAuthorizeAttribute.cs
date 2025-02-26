using Microsoft.AspNetCore.Mvc.Filters;
using OdiApp.BusinessLayer.Core.Exceptions;
using OdiApp.DTOs.Enums;

namespace Odi.Shared.AuthAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SuperAdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //giriş Yapılmamışsa
            if (!context.HttpContext.User.Identity.IsAuthenticated) throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");

            //// skip authorization if action is decorated with [AllowAnonymous] attribute
            //var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            //if (allowAnonymous)
            //    return;
            // authorization

            //giriş Yapılmamışsa
            var kayitGrublari = context.HttpContext.User.FindAll("KayitGrubuKodu");

            bool yetkiVar = false;
            foreach (var claim in kayitGrublari)
            {
                var kayitGrubu = claim.Value;
                if (kayitGrubu == KayitGrupKodlari.OdiYoneticisi)
                {
                    yetkiVar = true;
                    break; // Gerekli izin zaten bulunduğunda döngüden çıkın
                }
            }

            if (!yetkiVar)
            {
                throw new UnAuthorizeException("Bu işlem için yetkiniz bulunmamaktadır");
            }

        }
    }
}