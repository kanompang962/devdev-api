using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Users;
using devdev_api.Interfaces.Services.IJwt;
using Microsoft.IdentityModel.Tokens;

namespace devdev_api.Services.Jwt
{
    public class JwtService(IConfiguration _config) : IJwtService
    {

        public string GenerateAccessToken(User user, IEnumerable<string> roles)
        {
            var key     = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
            var creds   = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:AccessTokenMinutes"]!));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name,           user.Username),
                new(ClaimTypes.Email,          user.Email),
                new("firstName",               user.FirstName ?? ""),
                new("lastName",                user.LastName ?? ""),
                // new("companyId",               user.CompanyId?.ToString() ?? "")
            };

            // ใส่ roles ทุกตัวเป็น claim
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = new JwtSecurityToken(
                issuer:             _config["Jwt:Issuer"],
                audience:           _config["Jwt:Audience"],
                claims:             claims,
                expires:            expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }

        public int? GetUserIdFromToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt     = handler.ReadJwtToken(token);
                var idClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                return idClaim is null ? null : int.Parse(idClaim.Value);
            }
            catch { return null; }
        }
    }
}