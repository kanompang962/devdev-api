using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Domain.Entities.Masters
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long YearId { get; set; }
        public Year Year { get; set; } = null!;   
        public ICollection<Functional> Functionals { get; set; } = [];
    }
}