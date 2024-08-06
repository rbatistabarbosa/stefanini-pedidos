using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Stefanini.Pedidos.Api.Swagger
{
    [ExcludeFromCodeCoverage]
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
            => _apiVersionDescriptionProvider = apiVersionDescriptionProvider;

        public void Configure(SwaggerGenOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            var assembly = this.GetType().Assembly;
            var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
            var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

            foreach (var apiVersionDescription in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                var openApiInfo = new OpenApiInfo
                {
                    Description = assemblyDescription,
                    Title = assemblyProduct,
                    Version = apiVersionDescription.ApiVersion.ToString(),
                };

                if (apiVersionDescription.IsDeprecated)
                {
                    openApiInfo.Description += " - This API version has been deprecated.";
                }

                options.SwaggerDoc(apiVersionDescription.GroupName, openApiInfo);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter BEARER token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            }

            var filePath = Path.Combine(AppContext.BaseDirectory, $"{this.GetType().Assembly.GetName().Name}.xml");
            options.IncludeXmlComments(filePath);
        }
    }
}
