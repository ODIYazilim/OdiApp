using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OdiApp.BusinessLayer.Core.Filters
{
    public class AddLanguageRequiredHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// Swagger da Accept-Language parametresi için yazıldı
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Description = "English=odiDil-en;Türkçe=odiDil-tr",
                Required = true,
                //Schema = new OpenApiSchema
                //{
                //    Type = "String",
                //    Default = new OpenApiString("tr")
                //}
            });
        }
    }
}
