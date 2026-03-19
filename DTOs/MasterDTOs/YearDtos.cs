using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.DTOs.MasterDTOs
{

    // Response
    public record YearDto(
        long Id,
        bool IsActive,
        int YearValue 
    );

    // Create
    public record CreateYearDto
    {
        public int YearValue { get; init; }
    }
}