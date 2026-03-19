using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Auth;
using devdev_api.Domain.Entities.Users;
using devdev_api.DTOs.AuthDTOs;
using devdev_api.Interfaces.Repositories.IAuth;
using devdev_api.Interfaces.Repositories.IUser;
using devdev_api.Interfaces.Services.IAuth;
using devdev_api.Interfaces.Services.IJwt;

namespace devdev_api.Services.AuthServices
{
    public class AuthService(
        IUserRepo _userRepo,
        IConfiguration _config,
        IJwtService _jwtService,
        IRefreshTokenRepo _refreshTokenRepo
    ) : IAuthService
    {
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
        {
            if (await _userRepo.EmailExistsAsync(request.Email, ct))
                throw new InvalidOperationException("Email already exists.");

            if (await _userRepo.UsernameExistsAsync(request.Username, ct))
                throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                Username  = request.Username,
                Email     = request.Email,
                FirstName = request.FirstName,
                LastName  = request.LastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedBy = request.Email
                // CompanyId = request.CompanyId,
            };

            var created = await _userRepo.CreateAsync(user, ct);

            // assign default role = User
            await _userRepo.AssignRoleAsync(created.Id, roleId: 3, ct); // 1=Admin,2=Manager,3=User

            return await BuildAuthResponseAsync(created, ["User"], "", "", ct);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request, string ipAddress, string userAgent, CancellationToken ct = default)
        {
            // ✅ ดึง user พร้อม roles ในครั้งเดียว
            var user = await _userRepo.GetByEmailAsync(request.Email, ct)
                ?? throw new UnauthorizedAccessException("Invalid email or password.");

            if (user.IsLocked)
                throw new UnauthorizedAccessException("Account is locked. Please contact administrator.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                // นับ failed login
                user.FailedLoginCount += 1;
                if (user.FailedLoginCount >= 5) user.IsLocked = true;

                await _userRepo.UpdateAsync(user, ct);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // reset failed count
            user.FailedLoginCount = 0;
            user.LastLoginAt      = DateTimeOffset.UtcNow;
            await _userRepo.UpdateAsync(user, ct);

            var userWithRoles = await _userRepo.GetWithRolesAsync(user.Id, ct)
                ?? throw new KeyNotFoundException("User not found.");
            var roles         = userWithRoles!.UserRoles.Select(ur => ur.Role.Name).ToList();

            return await BuildAuthResponseAsync(user, roles, ipAddress, userAgent, ct);
        }

        public async Task<AuthResponse> RefreshTokenAsync(string refreshToken, string ipAddress, string userAgent, CancellationToken ct = default)
        {
            var token = await _refreshTokenRepo.GetByTokenAsync(refreshToken, ct)
                ?? throw new UnauthorizedAccessException("Invalid refresh token.");

            if (!token.IsActive)
                throw new UnauthorizedAccessException("Refresh token is expired or revoked.");

            // Rotate — revoke เก่า ออก token ใหม่
            var newRefreshToken = _jwtService.GenerateRefreshToken();
            token.IsRevoked       = true;
            token.RevokedReason   = "Rotated";
            token.ReplacedByToken = newRefreshToken;
            await _refreshTokenRepo.UpdateAsync(token, ct);

            var user      = await _userRepo.GetWithRolesAsync(token.UserId, ct)!;
            var roles     = user!.UserRoles.Select(ur => ur.Role.Name).ToList();
            var newAccess = _jwtService.GenerateAccessToken(user, roles);
            var expiresDays = int.Parse(_config["Jwt:RefreshTokenDays"]!);

            await _refreshTokenRepo.CreateAsync(new RefreshToken
            {
                UserId     = user.Id,
                Token      = newRefreshToken,
                ExpiresAt  = DateTimeOffset.UtcNow.AddDays(expiresDays),
                IpAddress  = ipAddress,
                UserAgent  = userAgent,
                CreatedBy  = user.Email
            }, ct);

            return new AuthResponse(
                newAccess,
                newRefreshToken,
                DateTimeOffset.UtcNow.AddMinutes(int.Parse(_config["Jwt:AccessTokenMinutes"]!)),
                ToUserInfo(user, roles));
        }

        public async Task RevokeTokenAsync(string refreshToken, string reason, CancellationToken ct = default)
        {
            var token = await _refreshTokenRepo.GetByTokenAsync(refreshToken, ct)
                ?? throw new KeyNotFoundException("Token not found.");

            if (!token.IsActive)
                throw new InvalidOperationException("Token is already revoked or expired.");

            token.IsRevoked     = true;
            token.RevokedReason = reason;
            await _refreshTokenRepo.UpdateAsync(token, ct);
        }

        public async Task LogoutAsync(int userId, CancellationToken ct = default)
        {
            // revoke ทุก token ของ user นี้
            await _refreshTokenRepo.RevokeAllUserTokensAsync(userId, "Logout", ct);
        }

        // ── Private ───────────────────────────────────────────────────────────────
        private async Task<AuthResponse> BuildAuthResponseAsync(
            User user, 
            IEnumerable<string> roles,
            string ipAddress, 
            string userAgent, 
            CancellationToken ct
        )
        {
            var accessToken   = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken  = _jwtService.GenerateRefreshToken();
            var expiresDays   = int.Parse(_config["Jwt:RefreshTokenDays"]!);
            var expiresMinutes = int.Parse(_config["Jwt:AccessTokenMinutes"]!);

            await _refreshTokenRepo.CreateAsync(new RefreshToken
            {
                UserId    = user.Id,
                Token     = refreshToken,
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(expiresDays),
                IpAddress = ipAddress,
                UserAgent = userAgent,
                CreatedBy = user.Email
            }, ct);

            return new AuthResponse(
                accessToken,
                refreshToken,
                DateTimeOffset.UtcNow.AddMinutes(expiresMinutes),
                ToUserInfo(user, roles));
        }

        private static UserInfoDto ToUserInfo(User user, IEnumerable<string> roles) => new(
            user.Id, 
            user.Username, 
            user.Email,
            user.FirstName, 
            user.LastName, 
            roles
        );
    }
}