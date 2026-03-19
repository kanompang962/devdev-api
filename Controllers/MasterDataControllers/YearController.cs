using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Common;
using devdev_api.DTOs;
using devdev_api.DTOs.MasterDTOs;
using devdev_api.Extensions;
using devdev_api.Interfaces.Services.IMaster;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace devdev_api.Controllers.MasterDataControllers
{

    public class YearController(
        IYearService _yearService,
        IValidator<CreateYearDto> _yearValidator
    ) : MasterDataControllerBase
    {

        [Authorize]
        [HttpGet("years")]
        public async Task<IActionResult> GetAllYear(
            [FromQuery] int page     = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            CancellationToken ct     = default)
        {
            var result = await _yearService.GetAllAsync(page, pageSize, search, ct);
            return Ok(ApiResponse<PagedResult<YearDto>>.Ok(result));
        }

        [Authorize]
        [HttpGet("years{id:long}")]
        public async Task<IActionResult> GetByIdYear(long id, CancellationToken ct)
        {
            var result = await _yearService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<YearDto>.Ok(result));
        }

        [Authorize]
        [HttpPost("years")]
        public async Task<IActionResult> CreateYear([FromBody] CreateYearDto dto, CancellationToken ct)
        {
            var (isValid, error) = await _yearValidator.ValidateRequestAsync(dto, ct);
            if (!isValid) return BadRequest(error);
            
            var result = await _yearService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetByIdYear), new { id = result.Id },
                ApiResponse<YearDto>.Ok(result, "Created successfully."));
        }

    }
}