using WebActivatorEx;
using RDataManager;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Collections.Generic;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace RDataManager
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null) operation.parameters = new List<Parameter>();
            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "access_token",
                required=false,
                type="string"
            });

        }
    }
}
