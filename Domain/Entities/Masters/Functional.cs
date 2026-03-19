using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Domain.Entities.Masters
{
    public class Functional : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public ICollection<Department> Departments { get; set; } = [];
    }
}