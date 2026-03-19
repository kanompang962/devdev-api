using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Interfaces.Services.IAuth;
using devdev_api.Interfaces.Services.IJwt;
using devdev_api.Interfaces.Services.IMaster;
using devdev_api.Interfaces.Services.IRiskProfiles;
using devdev_api.Services.AuthServices;
using devdev_api.Services.Jwt;
using devdev_api.Services.MasterServices;
using devdev_api.Services.RiskProfileServices;

namespace devdev_api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRiskProfileService, RiskProfileService>();
            services.AddScoped<IYearService, YearService>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}