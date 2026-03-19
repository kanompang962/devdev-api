using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Common;
using devdev_api.DTOs;
using devdev_api.DTOs.RiskProfileDTOs;
using devdev_api.Interfaces.Services.IRiskProfiles;
using Microsoft.AspNetCore.Mvc;
using devdev_api.Extensions;

namespace devdev_api.Controllers.RiskProfileControllers
{
    [Route("[controller]")]
    public class RiskProfileController(IRiskProfileService _riskProfileService) : Controller
    {


        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page     = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            CancellationToken ct     = default)
        {
            var result = await _riskProfileService.GetAllAsync(page, pageSize, search, ct);
            return Ok(ApiResponse<PagedResult<RiskProfileDto>>.Ok(result));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
        {
            var result = await _riskProfileService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<RiskProfileDto>.Ok(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRiskProfileDto dto, CancellationToken ct)
        {
            var result = await _riskProfileService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<RiskProfileDto>.Ok(result, "Created successfully."));
        }
    }
}