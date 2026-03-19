using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.RiskProfiles;
using devdev_api.DTOs.RiskProfileDTOs;
using Riok.Mapperly.Abstractions;

namespace devdev_api.Mappings.RiskProfileMappers
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)] // สั่งให้ ignore ตัวที่ไม่มีคู่แมพฝั่งต้นทาง
    public partial class RiskProfileMapper
    {
        // Response
        public partial RiskProfileDto ToDto(RiskProfile entity);
        public partial IReadOnlyList<RiskProfileDto> ToDtoList(IReadOnlyList<RiskProfile> entities);

        // Create 
        public partial RiskProfile ToEntity(CreateRiskProfileDto dto);

        // Update
        public partial void UpdateEntity(UpdateRiskProfileDto dto, RiskProfile entity);
    }
}