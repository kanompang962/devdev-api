using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.DTOs;
using devdev_api.DTOs.MasterDTOs;

namespace devdev_api.Interfaces.Services.IMaster
{
    public interface IYearService
    {
        Task<PagedResult<YearDto>> GetAllAsync(int page, int pageSize, string? search, CancellationToken ct = default);
        Task<YearDto> GetByIdAsync(long id, CancellationToken ct = default);
        Task<YearDto> CreateAsync(CreateYearDto dto, CancellationToken ct = default);
    }
}