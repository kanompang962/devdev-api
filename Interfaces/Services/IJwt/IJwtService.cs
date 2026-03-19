using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Users;

namespace devdev_api.Interfaces.Services.IJwt
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user, IEnumerable<string> roles);
        string GenerateRefreshToken();
        int? GetUserIdFromToken(string token);
    }
}