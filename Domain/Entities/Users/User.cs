using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Auth;
using devdev_api.Domain.Entities.Masters;

namespace devdev_api.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsLocked { get; set; } = false;
        public int FailedLoginCount { get; set; } = 0;
        public DateTimeOffset? LastLoginAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = [];
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}