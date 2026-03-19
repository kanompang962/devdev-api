using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Data;
using devdev_api.Domain.Entities.Auth;
using devdev_api.Interfaces.Repositories.IAuth;
using Microsoft.EntityFrameworkCore;

namespace devdev_api.Repositories.AuthRepository
{
    public class RefreshTokenRepo(AppDbContext _db) : IRefreshTokenRepo
    {
        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default)
            => await _db.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token, ct);

        public async Task CreateAsync(RefreshToken token, CancellationToken ct = default)
        {
            await _db.RefreshTokens.AddAsync(token, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(RefreshToken token, CancellationToken ct = default)
        {
            _db.RefreshTokens.Update(token);
            await _db.SaveChangesAsync(ct);
        }

        public async Task RevokeAllUserTokensAsync(int userId, string reason, CancellationToken ct = default)
        {
            var tokens = await _db.RefreshTokens
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .ToListAsync(ct);

            foreach (var token in tokens)
            {
                token.IsRevoked     = true;
                token.RevokedReason = reason;
            }

            await _db.SaveChangesAsync(ct);
        }
    }
}