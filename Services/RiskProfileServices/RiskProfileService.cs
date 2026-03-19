using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.DTOs;
using devdev_api.DTOs.RiskProfileDTOs;
using devdev_api.Interfaces.Repositories.IRiskProfiles;
using devdev_api.Interfaces.Services.IRiskProfiles;
using devdev_api.Mappings.RiskProfileMappers;

namespace devdev_api.Services.RiskProfileServices
{
    public class RiskProfileService(
        RiskProfileMapper _mapper,
        IRiskProfileRepo _riskProfileRepo
    ) : IRiskProfileService
    {

        public async Task<PagedResult<RiskProfileDto>> GetAllAsync(int page, int pageSize, string? search, CancellationToken ct = default)
        {
            var (items, total) = await _riskProfileRepo.GetPagedAsync(page, pageSize, search, ct);
            var dtos = _mapper.ToDtoList(items);
            return new PagedResult<RiskProfileDto>(dtos, total, page, pageSize);
        }

        public async Task<RiskProfileDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _riskProfileRepo.GetByIdAsync(id, ct)
                ?? throw new KeyNotFoundException($"Risk Profile '{id}' not found.");
            return _mapper.ToDto(entity);
        }

        public async Task<RiskProfileDto> CreateAsync(CreateRiskProfileDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.ToEntity(dto);
            var created = await _riskProfileRepo.CreateAsync(entity, ct);
            return _mapper.ToDto(created);
        }
    }
}