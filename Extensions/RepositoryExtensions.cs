using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Interfaces.Repositories.IAuth;
using devdev_api.Interfaces.Repositories.IMaster;
using devdev_api.Interfaces.Repositories.IRiskProfiles;
using devdev_api.Interfaces.Repositories.IUser;
using devdev_api.Repositories.AuthRepository;
using devdev_api.Repositories.MasterRepository;
using devdev_api.Repositories.RiskProfileRepository;
using devdev_api.Repositories.UserRepository;

namespace devdev_api.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRiskProfileRepo, RiskProfileRepo>();
            services.AddScoped<IYearRepo, YearRepo>();
            services.AddScoped<ICompanyRepo, CompanyRepo>();

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();

            return services;
        }
    }
}