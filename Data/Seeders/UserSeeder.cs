using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Users;
using devdev_api.Interfaces.Services.IDataSeeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace devdev_api.Data.Seeders
{
    public class UserSeeder(AppDbContext db) : IDataSeeder
    {
        private readonly AppDbContext _db = db;

        public int Order => 3;

        public async Task SeedAsync()
        {
            // 👉 ดึง role จาก DB
            var roles = await _db.Roles.ToDictionaryAsync(r => r.Name);

            // 👉 list user ที่ต้องมี
            var users = new[]
            {
                new { Email = "admin@email.com", Role = "Admin", FirstName = "Admin", LastName = "System" },
                new { Email = "user@email.com",  Role = "User",  FirstName = "User", LastName = "System" }
            };

            foreach (var u in users)
            {
                var exists = await _db.Users
                    .Include(x => x.UserRoles)
                    .FirstOrDefaultAsync(x => x.Email == u.Email);

                if (exists != null)
                    continue;

                var user = new User
                {
                    Username = u.Email,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("1")
                };
                // user.PasswordHash = _passwordHasher.HashPassword(user, "1");

                _db.Users.Add(user);
                await _db.SaveChangesAsync(); // ต้อง save ก่อนเพื่อได้ Id

                // 👉 assign role
                if (roles.TryGetValue(u.Role, out var role))
                {
                    _db.UserRoles.Add(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    });
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}