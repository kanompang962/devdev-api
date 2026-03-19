using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using devdev_api.Common;
using devdev_api.DTOs.AuthDTOs;
using devdev_api.Extensions;
using devdev_api.Interfaces.Services.IAuth;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace devdev_api.Controllers.AuthControllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(
        IConfiguration _config,
        IAuthService _authService,
        IValidator<LoginRequest> _loginValidator
        ) : ControllerBase
    {
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
        {
            var result = await _authService.RegisterAsync(request, ct);
            return Ok(ApiResponse<AuthResponse>.Ok(result, "Registered successfully."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var (isValid, error) = await _loginValidator.ValidateRequestAsync(request, ct);
            if (!isValid) return BadRequest(error);

            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault()
                ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                ?? "";
            var userAgent = Request.Headers.UserAgent.ToString();
            var result    = await _authService.LoginAsync(request, ip, userAgent, ct);
            SetRefreshTokenCookie(result.RefreshToken); // ✅ set cookie ได้ "2MczLKD8MRA4uJyMcuKS/xUbl1P+hx1mkZR04SBmDhBd4HhgrVZUhdV2aDufiWugdtwgtJ9GnrstSEeISawImw=="
            var response = result with { RefreshToken = string.Empty };
            return Ok(ApiResponse<AuthResponse>.Ok(response, "Login successful."));
        }

        [AllowAnonymous] // 🔥 สำคัญมาก
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(CancellationToken ct)
        {
            var refreshToken = Request.Cookies["refreshToken"]
                ?? throw new UnauthorizedAccessException("Refresh token not found.");
            // ไม่เจอ refreshToken
            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault()
                ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                ?? "";
            var userAgent = Request.Headers.UserAgent.ToString();
            var result    = await _authService.RefreshTokenAsync(refreshToken, ip, userAgent, ct);
            SetRefreshTokenCookie(result.RefreshToken); // ✅ replace cookie
            var response = result with { RefreshToken = string.Empty };
            return Ok(ApiResponse<AuthResponse >.Ok(response, "Token refreshed."));
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(CancellationToken ct)
        {
            var refreshToken = Request.Cookies["refreshToken"]
                ?? throw new UnauthorizedAccessException("Refresh token not found.");

            await _authService.RevokeTokenAsync(refreshToken, "Revoked by user", ct);
            DeleteRefreshTokenCookie();  // ✅ ใช้ method เดียวกัน

            return Ok(ApiResponse<object>.Ok(new { }, "Token revoked."));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            // ✅ Revoke token ใน DB ผ่าน userId
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("Invalid token.");
            var userId = int.Parse(userIdClaim);
            await _authService.LogoutAsync(userId, ct);
            DeleteRefreshTokenCookie();
            return Ok(ApiResponse<object>.Ok(new { }, "Logged out successfully."));
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var result = new
            {
                Id          = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Username    = User.FindFirstValue(ClaimTypes.Name),
                Email       = User.FindFirstValue(ClaimTypes.Email),
                FirstName   = User.FindFirstValue("firstName"),
                LastName    = User.FindFirstValue("lastName"),
                Roles       = User.FindAll(ClaimTypes.Role).Select(c => c.Value)
            };
            return Ok(ApiResponse<object>.Ok(result));
        }

        // ── Cookie Helpers ────────────────────────────────────────────────────────────
        private void SetRefreshTokenCookie(string token)
            => Response.Cookies.Append("refreshToken", token, GetCookieOptions());

        private void DeleteRefreshTokenCookie()
            => Response.Cookies.Delete("refreshToken", GetCookieOptions(delete: true));

        private CookieOptions GetCookieOptions(bool delete = false)
        {
            var expiresDays = int.Parse(_config["Jwt:RefreshTokenDays"]!);
            return new CookieOptions
            {
                HttpOnly = true,
                Secure   = false,             // dev: false / prod: true
                SameSite = SameSiteMode.Lax,  // dev: Lax   / prod: Strict
                Path     = "/",               // ✅ "/" — ส่ง cookie ทุก endpoint
                Expires  = delete
                    ? DateTimeOffset.UtcNow.AddDays(-1)          // ✅ ลบทันที
                    : DateTimeOffset.UtcNow.AddDays(expiresDays) // ✅ set อายุ
            };
        }
    }
}