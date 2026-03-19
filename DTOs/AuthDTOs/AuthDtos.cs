using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.DTOs.AuthDTOs
{
    public record RegisterRequest(
        string Username,
        string Email,
        string Password,
        string? FirstName,
        string? LastName
    );

    public record LoginRequest(
        string Email,
        string Password
    );

    public record RefreshTokenRequest(
        string RefreshToken
    );

    public record RevokeTokenRequest(
        string RefreshToken
    );

    public record AuthResponse(
        string AccessToken,
        string RefreshToken,
        DateTimeOffset AccessTokenExpires,
        UserInfoDto User
    );

    public record UserInfoDto(
        long Id,
        string Username,
        string Email,
        string? FirstName,
        string? LastName,
        IEnumerable<string> Roles
    );
}