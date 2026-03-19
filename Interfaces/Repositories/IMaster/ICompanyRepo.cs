using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Masters;

namespace devdev_api.Interfaces.Repositories.IMaster
{
    public interface ICompanyRepo
    {
        Task<(IReadOnlyList<Company> Items, int Total)> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default);
        Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken ct = default);
        Task<Company?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<Company> CreateAsync(Company entity, CancellationToken ct = default);
    }
}