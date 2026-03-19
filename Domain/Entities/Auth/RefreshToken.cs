using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Users;

namespace devdev_api.Domain.Entities.Auth
{
    public class RefreshToken : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public string Token { get; set; } = string.Empty;
        public DateTimeOffset ExpiresAt { get; set; }
        public bool IsRevoked { get; set; } = false;
        public string? RevokedReason { get; set; }
        public string? ReplacedByToken { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;

        public bool IsExpired => DateTimeOffset.UtcNow >= ExpiresAt;
        public new bool IsActive  => !IsRevoked && !IsExpired;
    }
}