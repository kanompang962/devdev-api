using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Mappings.RiskProfileMappers;

namespace devdev_api.Extensions
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<RiskProfileMapper>()
                .AddClasses(classes => classes.InNamespaces("devdev_api.Mappings"))
                .AsSelf()
                .WithSingletonLifetime());

            return services;
        }
    }
}