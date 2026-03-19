using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace devdev_api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DevDev API",
                    Version = "v1"
                });

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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

                opt.TagActionsBy(api =>
                {
                    // ใช้ [Tags] attribute ถ้ามี
                    var tags = api.ActionDescriptor.EndpointMetadata
                        .OfType<TagsAttribute>()
                        .FirstOrDefault()?.Tags;

                    if (tags?.Count > 0)
                        return [.. tags];

                    // fallback ใช้ controller name
                    return [api.ActionDescriptor.RouteValues["controller"] ?? "Default"];
                });

                opt.DocInclusionPredicate((name, api) => true);
            });

            return services;
        }
    }
}