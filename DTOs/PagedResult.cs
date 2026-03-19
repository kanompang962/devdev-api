using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.DTOs
{
    public record PagedResult<T>(
        IReadOnlyList<T> Items,
        int TotalCount,
        int Page,
        int PageSize
    )
    {
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNext   => Page < TotalPages; 
        public bool HasPrev   => Page > 1;  
    };
}