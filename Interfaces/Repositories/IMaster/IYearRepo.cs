using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Masters;

namespace devdev_api.Interfaces.Repositories.IMaster
{
    public interface IYearRepo
    {
        Task<(IReadOnlyList<Year> Items, int Total)> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default);
        Task<IReadOnlyList<Year>> GetAllAsync(CancellationToken ct = default);
        Task<Year?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<Year> CreateAsync(Year entity, CancellationToken ct = default);
    }
}