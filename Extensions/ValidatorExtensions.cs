using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Common;
using devdev_api.Validators;
using devdev_api.Validators.AuthValidators;
using FluentValidation;

namespace devdev_api.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ValidationAssemblyMarker).Assembly);

            return services;
        }
    }
}