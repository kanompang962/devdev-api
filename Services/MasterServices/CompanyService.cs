using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.DTOs;
using devdev_api.DTOs.MasterDTOs;
using devdev_api.Interfaces.Repositories.IMaster;
using devdev_api.Interfaces.Services.IMaster;
using devdev_api.Mappings.MasterMappers;

namespace devdev_api.Services.MasterServices
{
    public class CompanyService(
        CompanyMapper _mapper,
        IYearRepo _yearRepo,
        ICompanyRepo _companyRepo
    ) : ICompanyService
    {

        public async Task<PagedResult<CompanyDto>> GetAllAsync(int page, int pageSize, string? search, CancellationToken ct = default)
        {
            var (items, total) = await _companyRepo.GetPagedAsync(page, pageSize, search, ct);
            var dtos = _mapper.ToDtoList(items);
            return new PagedResult<CompanyDto>(dtos, total, page, pageSize);
        }

        public async Task<CompanyDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _companyRepo.GetByIdAsync(id, ct)
                ?? throw new KeyNotFoundException($"Company '{id}' not found.");
            return _mapper.ToDto(entity);
        }

        public async Task<CompanyDto> CreateAsync(CreateCompanyDto dto, CancellationToken ct = default)
        {
            var existingYear = await _yearRepo.GetByIdAsync(dto.YearId, ct)
                ?? throw new KeyNotFoundException($"Year '{dto.YearId}' not found.");

            var entity = _mapper.ToEntity(dto);
            var created = await _companyRepo.CreateAsync(entity, ct);
            return _mapper.ToDto(created);
        }
    }
}