using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Filters;
using devdev_api.Infrastructure;

namespace devdev_api.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                opt.Conventions.Add(new RoutePrefixConvention("api"));
                opt.Filters.Add<ValidationFilter>();
            });
            services.AddEndpointsApiExplorer();

            return services;
        }
    }   
}