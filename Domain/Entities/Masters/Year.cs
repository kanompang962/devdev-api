using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Domain.Entities.Masters
{
    public class Year : BaseEntity
    {
        public int YearValue { get; set; }
        public bool IsCurrent { get; set; } = false;
        public ICollection<Company> Companies { get; set; } = [];
    }
}