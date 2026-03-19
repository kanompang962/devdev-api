using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.DTOs;
using devdev_api.DTOs.RiskProfileDTOs;

namespace devdev_api.Interfaces.Services.IRiskProfiles
{
    public interface IRiskProfileService
    {
        Task<PagedResult<RiskProfileDto>> GetAllAsync(int page, int pageSize, string? search, CancellationToken ct = default);
        Task<RiskProfileDto> GetByIdAsync(long id, CancellationToken ct = default);
        Task<RiskProfileDto> CreateAsync(CreateRiskProfileDto dto, CancellationToken ct = default);
    }
}