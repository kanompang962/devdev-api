using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devdev_api.Domain.Entities.Users
{
    public class UserRole
    {
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public long RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}