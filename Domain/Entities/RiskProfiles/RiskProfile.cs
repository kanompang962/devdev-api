using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Domain.Entities.RiskProfiles
{
    public class RiskProfile : BaseEntity
    {
        public string DocumentNo { get; set; } = string.Empty;
        public int DocumentStatusId  { get; set; } = 1;
        public int CompanyId { get; set; }
        public int? FunctionalId { get; set; }
        public int? DepartmentId { get; set; }
    }
}