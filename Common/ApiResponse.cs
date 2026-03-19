using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        // ── Static factories ──────────────────────────────────────────────────────

        public static ApiResponse<T> Ok(T data, string message = "Success") => new()
        {
            Success   = true,
            Message   = message,
            Data      = data
        };

        public static ApiResponse<T> Fail(string message, List<string>? errors = null) => new()
        {
            Success   = false,
            Message   = message,
            Errors    = errors
        };
    }

    public class PagedApiResponse<T> : ApiResponse<PagedData<T>>
    {
        public static PagedApiResponse<T> Ok(IEnumerable<T> items, int total, int page, int pageSize, string message = "Success")
        {
            var totalPages = (int)Math.Ceiling((double)total / pageSize);
            return new PagedApiResponse<T>
            {
                Success = true,
                Message = message,
                Data    = new PagedData<T>
                {
                    Items      = items,
                    Total      = total,
                    Page       = page,
                    PageSize   = pageSize,
                    TotalPages = totalPages,
                    HasNext    = page < totalPages,
                    HasPrev    = page > 1
                }
            };
        }
    }

    public class PagedData<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
    }
}