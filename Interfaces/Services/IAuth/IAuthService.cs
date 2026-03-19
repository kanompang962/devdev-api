using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.DTOs.AuthDTOs;

namespace devdev_api.Interfaces.Services.IAuth
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
        Task<AuthResponse> LoginAsync(LoginRequest request, string ipAddress, string userAgent, CancellationToken ct = default);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken, string ipAddress, string userAgent, CancellationToken ct = default);
        Task RevokeTokenAsync(string refreshToken, string reason, CancellationToken ct = default);
        Task LogoutAsync(int userId, CancellationToken ct = default);
    }
}