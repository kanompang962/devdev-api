using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Masters;
using devdev_api.DTOs.MasterDTOs;
using Riok.Mapperly.Abstractions;

namespace devdev_api.Mappings.MasterMappers
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)] // สั่งให้ ignore ตัวที่ไม่มีคู่แมพฝั่งต้นทาง
    public partial class YearMapper
    {
        // Response
        public partial YearDto ToDto(Year entity);
        public partial IReadOnlyList<YearDto> ToDtoList(IReadOnlyList<Year> entities);

        // Create 
        public partial Year ToEntity(CreateYearDto dto);

        // Update
        // public partial void UpdateEntity(UpdateRiskProfileDto dto, RiskProfile entity);
    }
}