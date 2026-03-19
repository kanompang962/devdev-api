using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Domain.Entities.Masters
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long FunctionalId { get; set; }
        public Functional Functional { get; set; } = null!;
    }
}