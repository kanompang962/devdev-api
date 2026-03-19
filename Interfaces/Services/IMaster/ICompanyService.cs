using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.DTOs;
using devdev_api.DTOs.MasterDTOs;

namespace devdev_api.Interfaces.Services.IMaster
{
    public interface ICompanyService
    {
        Task<PagedResult<CompanyDto>> GetAllAsync(int page, int pageSize, string? search, CancellationToken ct = default);
        Task<CompanyDto> GetByIdAsync(long id, CancellationToken ct = default);
        Task<CompanyDto> CreateAsync(CreateCompanyDto dto, CancellationToken ct = default);
    }
}