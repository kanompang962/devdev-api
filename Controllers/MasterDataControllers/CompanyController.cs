using System;
using System.Collections.Generic;
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

    public class CompanyController(
        ICompanyService _companyService,
        IValidator<CreateCompanyDto> _companyValidator
    ) : MasterDataControllerBase
    {
        [Authorize]
        [HttpGet("companies")]
        public async Task<IActionResult> GetAllCompany(
            [FromQuery] int page     = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            CancellationToken ct     = default)
        {
            var result = await _companyService.GetAllAsync(page, pageSize, search, ct);
            return Ok(ApiResponse<PagedResult<CompanyDto>>.Ok(result));
        }

        [Authorize]
        [HttpGet("companies{id:long}")]
        public async Task<IActionResult> GetByIdCompany(long id, CancellationToken ct)
        {
            var result = await _companyService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<CompanyDto>.Ok(result));
        }

        [Authorize]
        [HttpPost("companies")]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto dto, CancellationToken ct)
        {
            var (isValid, error) = await _companyValidator.ValidateRequestAsync(dto, ct);
            if (!isValid) return BadRequest(error);
            
            var result = await _companyService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetByIdCompany), new { id = result.Id },
                ApiResponse<CompanyDto>.Ok(result, "Created successfully."));
        }
    }
}