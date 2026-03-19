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
    public class YearService(
        YearMapper _mapper,
        IYearRepo _yearRepo
    ) : IYearService
    {

        public async Task<PagedResult<YearDto>> GetAllAsync(int page, int pageSize, string? search, CancellationToken ct = default)
        {
            var (items, total) = await _yearRepo.GetPagedAsync(page, pageSize, search, ct);
            var dtos = _mapper.ToDtoList(items);
            return new PagedResult<YearDto>(dtos, total, page, pageSize);
        }

        public async Task<YearDto> GetByIdAsync(long id, CancellationToken ct = default)
        {
            var entity = await _yearRepo.GetByIdAsync(id, ct)
                ?? throw new KeyNotFoundException($"Year '{id}' not found.");
            return _mapper.ToDto(entity);
        }

        public async Task<YearDto> CreateAsync(CreateYearDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.ToEntity(dto);
            var created = await _yearRepo.CreateAsync(entity, ct);
            return _mapper.ToDto(created);
        }
    }
}