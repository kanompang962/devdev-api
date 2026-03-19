using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAutoDependencies(this IServiceCollection services)
        {
            var assembly = typeof(Program).Assembly;

            services.Scan(scan => scan
                .FromAssemblies(assembly)

                .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(c => c.Where(t => t.Name.EndsWith("Repo")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(c => c.Where(t => t.Name.EndsWith("Mapper")))
                .AsSelf()
                .WithSingletonLifetime()
            );

            return services;
        }
    }
}