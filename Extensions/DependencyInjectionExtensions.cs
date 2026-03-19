using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Users;
using devdev_api.Interfaces.Services.IDataSeeders;
using devdev_api.Services.BackgroundServices;
using Microsoft.AspNetCore.Identity;

namespace devdev_api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAutoDependencies(this IServiceCollection services)
        {
            var assembly = typeof(Program).Assembly;

            services.AddHostedService<YearBackgroundService>();

            services.Scan(scan => scan
                .FromAssemblies(assembly)

                .AddClasses(c => c.Where(t => t.Name.EndsWith("Service") && 
                    !typeof(IHostedService).IsAssignableFrom(t)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(c => c.Where(t => t.Name.EndsWith("Repo")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(c => c.Where(t => t.Name.EndsWith("Mapper")))
                .AsSelf()
                .WithSingletonLifetime()

                .AddClasses(c => c.AssignableTo<IDataSeeder>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            return services;
        }
    }
}