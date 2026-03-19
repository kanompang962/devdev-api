using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Auth;

namespace devdev_api.Interfaces.Repositories.IAuth
{
    public interface IRefreshTokenRepo
    {
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default);
        Task CreateAsync(RefreshToken token, CancellationToken ct = default);
        Task UpdateAsync(RefreshToken token, CancellationToken ct = default);
        Task RevokeAllUserTokensAsync(int userId, string reason, CancellationToken ct = default);
    }
}