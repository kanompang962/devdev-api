using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.RiskProfiles;
using devdev_api.DTOs;

namespace devdev_api.Interfaces.Repositories.IRiskProfiles
{
    public interface IRiskProfileRepo
    {
        Task<IReadOnlyList<RiskProfile>> GetAllAsync(CancellationToken ct = default);
        Task<(IReadOnlyList<RiskProfile> Items, int Total)> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default);
        Task<RiskProfile?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<RiskProfile> CreateAsync(RiskProfile entity, CancellationToken ct = default);
        Task UpdateAsync(RiskProfile entity, CancellationToken ct = default);
        Task DeleteAsync(RiskProfile entity, CancellationToken ct = default);
        Task<bool> ExistsAsync(long id, CancellationToken ct = default);
    }
}