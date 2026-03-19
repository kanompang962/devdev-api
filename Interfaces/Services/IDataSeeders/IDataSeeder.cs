using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Interfaces.Services.IDataSeeders
{
    public interface IDataSeeder
    {
        int Order { get; }
        Task SeedAsync();
    }
}