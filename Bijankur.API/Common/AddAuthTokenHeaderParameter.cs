using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen;
using Swashbuckle.SwaggerGen.Generator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.API.Common
{
    public class AddAuthTokenHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();


            //operation.Parameters.Add(new NonBodyParameter()
            //{
            //    Name = "apptoken",
            //    In = "header",
            //    Type = "string",
            //    Required = true
            //});

            if (OperationParameterService.IsShowUserToken(operation))
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "string",
                    Required = true
                });
            }

            Debug.WriteLine(operation.OperationId.ToLower());
        }
    }

    public static class OperationParameterService
    {
        public static bool IsShowUserToken(Operation operation)
        {
            bool result = true;
            if (operation.OperationId.ToLower() == "apiuserauthenticatepost")
            {
                result = false;
            }
            return result;
        }
    }
}
