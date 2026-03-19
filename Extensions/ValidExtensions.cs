using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace devdev_api.Extensions
{
    public static class ValidExtensions
    {
        public static IServiceCollection AddValids(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();

            return services;
        }
    }
}