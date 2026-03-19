using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Interfaces.Services.IAuth;

namespace devdev_api.Services.AuthServices
{
    public class CookieService(IConfiguration config) : ICookieService
    {
        private readonly IConfiguration _config = config;

        public void SetRefreshToken(HttpResponse response, string token)
        {
            response.Cookies.Append("refreshToken", token, GetOptions());
        }

        public void DeleteRefreshToken(HttpResponse response)
        {
            response.Cookies.Delete("refreshToken", GetOptions(true));
        }

        private CookieOptions GetOptions(bool delete = false)
        {
            var expiresDays = int.Parse(_config["Jwt:RefreshTokenDays"]!);

            return new CookieOptions
            {
                HttpOnly = true,
                Secure   = true,
                SameSite = SameSiteMode.Strict,
                Path     = "/",
                Expires  = delete
                    ? DateTimeOffset.UtcNow.AddDays(-1)
                    : DateTimeOffset.UtcNow.AddDays(expiresDays)
            };
        }
    }
}