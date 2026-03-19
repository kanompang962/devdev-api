using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Users;

namespace devdev_api.Interfaces.Repositories.IUser
{
    public interface IUserRepo
    {
        Task<User?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<User?> GetWithRolesAsync(long id, CancellationToken ct = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
        Task<bool> UsernameExistsAsync(string username, CancellationToken ct = default);
        Task<User> CreateAsync(User user, CancellationToken ct = default);
        Task UpdateAsync(User user, CancellationToken ct = default);
        Task AssignRoleAsync(long userId, long roleId, CancellationToken ct = default);
    }
}