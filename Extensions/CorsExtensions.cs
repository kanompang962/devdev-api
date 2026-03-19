using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Extensions
{
    public static class CorsExtensions
    {
        private const string Policy = "AllowFrontend";

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(Policy, policy =>
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });

            return services;
        }

        public static WebApplication UseCorsPolicy(this WebApplication app)
        {
            app.UseCors(Policy);
            return app;
        }
    }
}