using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Data.Seeders;
using devdev_api.Interfaces.Services.IDataSeeders;

namespace devdev_api.Services.BackgroundServices
{
    public class YearBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var seeders = scope.ServiceProvider
                    .GetServices<IDataSeeder>()
                    .OrderBy(s => s.Order);

                foreach (var seeder in seeders)
                {
                    await seeder.SeedAsync();
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}