
using System.Security.Claims;
using devdev_api.Common;
using devdev_api.DTOs.AuthDTOs;
using devdev_api.Extensions;
using devdev_api.Interfaces.Services.IAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace devdev_api.Controllers.AuthControllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(
        IAuthService _authService,
        ICookieService _cookieService
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
            var (ip, userAgent) = HttpContext.GetClientInfo();
            var result    = await _authService.LoginAsync(request, ip, userAgent, ct);
            _cookieService.SetRefreshToken(Response, result.RefreshToken);
            return Ok(ApiResponse<AuthResponse>.Ok(
                result with { RefreshToken = string.Empty },
                "Login successful."));
        }

        [AllowAnonymous] // 🔥 สำคัญมาก
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(CancellationToken ct)
        {
            var refreshToken = Request.Cookies["refreshToken"]
                ?? throw new UnauthorizedAccessException("Refresh token not found.");
            var (ip, userAgent) = HttpContext.GetClientInfo();
            var result    = await _authService.RefreshTokenAsync(refreshToken, ip, userAgent, ct);
           _cookieService.SetRefreshToken(Response, result.RefreshToken);
            return Ok(ApiResponse<AuthResponse>.Ok(
                result with { RefreshToken = string.Empty },
                "Token refreshed."));
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(CancellationToken ct)
        {
            var refreshToken = Request.Cookies["refreshToken"]
                ?? throw new UnauthorizedAccessException("Refresh token not found.");
            await _authService.RevokeTokenAsync(refreshToken, "Revoked by user", ct);
             _cookieService.DeleteRefreshToken(Response);
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
            _cookieService.DeleteRefreshToken(Response);
            return Ok(ApiResponse<object>.Ok(new { }, "Logged out successfully."));
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(ApiResponse<object>.Ok(new
            {
                Id        = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Username  = User.FindFirstValue(ClaimTypes.Name),
                Email     = User.FindFirstValue(ClaimTypes.Email),
                FirstName = User.FindFirstValue("firstName"),
                LastName  = User.FindFirstValue("lastName"),
                Roles     = User.FindAll(ClaimTypes.Role).Select(c => c.Value)
            }));
        }

    }
}