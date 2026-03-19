using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.DTOs.MasterDTOs
{

    // Response
    public record CompanyDto(
        long Id,
        bool IsActive,
        string Name,
        string Code,
        string? Description,
        long YearId
    );

    // Create
    public record CreateCompanyDto
    {
        public string Name { get; init; } = default!;
        public string Code { get; init; } = default!;
        public long YearId { get; init; }
        public string? Description { get; init; }
    }
}