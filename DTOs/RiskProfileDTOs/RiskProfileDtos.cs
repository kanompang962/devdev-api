using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.DTOs.RiskProfileDTOs
{
    public record RiskProfileDto(
        long Id,
        bool IsActive,
        string CreatedBy,
        string? UpdatedBy,
        DateTimeOffset CreatedAt,
        DateTimeOffset? UpdatedAt,
        string DocumentNo,
        int DocumentStatusId,
        int CompanyId ,
        int? FunctionalId ,
        int? DepartmentId 
    );

    // Create
    public record CreateRiskProfileDto(
        int CompanyId ,
        int? FunctionalId ,
        int? DepartmentId 
    );

    // Update
    public record UpdateRiskProfileDto(
        string DocumentNo,
        int DocumentStatus
    );
}