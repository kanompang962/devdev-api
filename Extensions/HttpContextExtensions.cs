using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Extensions
{
    public static class HttpContextExtensions
    {
        public static (string Ip, string UserAgent) GetClientInfo(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                ?? context.Connection.RemoteIpAddress?.ToString()
                ?? "";

            var userAgent = context.Request.Headers.UserAgent.ToString();

            return (ip, userAgent);
        }
    }
}