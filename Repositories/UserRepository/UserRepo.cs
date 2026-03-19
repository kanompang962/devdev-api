using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Data;
using devdev_api.Domain.Entities.Users;
using devdev_api.Interfaces.Repositories.IUser;
using Microsoft.EntityFrameworkCore;

namespace devdev_api.Repositories.UserRepository
{
    public class UserRepo(AppDbContext _db) : IUserRepo
    {
       public async Task<User?> GetByIdAsync(long id, CancellationToken ct = default)
        => await _db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
            => await _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

        public async Task<User?> GetWithRolesAsync(long id, CancellationToken ct = default)
            => await _db.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id, ct);

        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
            => await _db.Users.AnyAsync(u => u.Email == email, ct);

        public async Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default)
            => await _db.Users.AnyAsync(u => u.Username == username, ct);

        public async Task<User> CreateAsync(User user, CancellationToken ct = default)
        {
            await _db.Users.AddAsync(user, ct);
            await _db.SaveChangesAsync(ct);
            return user;
        }

        public async Task UpdateAsync(User user, CancellationToken ct = default)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync(ct);
        }

        public async Task AssignRoleAsync(long userId, long roleId, CancellationToken ct = default)
        {
            var userRole = new UserRole { UserId = userId, RoleId = roleId };
            await _db.UserRoles.AddAsync(userRole, ct);
            await _db.SaveChangesAsync(ct);
        }
    }
}