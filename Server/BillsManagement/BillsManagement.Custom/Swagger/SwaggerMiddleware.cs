//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BillsManagement.Custom.Swagger
//{
//    public static class SwaggerMiddleware
//    {
//        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
//        {             //...add 
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc(_assemblyVersion, new OpenApiInfo { Title = _assemblyName, Version = _assemblyVersion });
//                c.IncludeXmlComments($"{_applicationBaseDirectory}\\{ApiConstants.CoreXmlDocumentation}");
//                c.IncludeXmlComments($"{_applicationBaseDirectory}\\{ApiConstants.DomainModelsXmlDocumentation}");
//            });
//            return services;
//        }
//    }
//}
