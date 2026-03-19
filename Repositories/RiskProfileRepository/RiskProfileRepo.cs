using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Data;
using devdev_api.Domain.Entities.RiskProfiles;
using devdev_api.Interfaces.Repositories.IRiskProfiles;
using Microsoft.EntityFrameworkCore;

namespace devdev_api.Repositories.RiskProfileRepository
{
    public class RiskProfileRepo(AppDbContext db) : IRiskProfileRepo
    {
        private readonly AppDbContext _db = db;

        public async Task<IReadOnlyList<RiskProfile>> GetAllAsync(CancellationToken ct = default)
            => await _db.RiskProfiles.ToListAsync(ct);

        public async Task<(IReadOnlyList<RiskProfile> Items, int Total)> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default)
        {
            var query = _db.RiskProfiles.AsQueryable();

            // if (!string.IsNullOrWhiteSpace(search))
            //     query = query.Where(p =>
            //         p.Name.Contains(search) ||
            //         p.Description.Contains(search));

            var total = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<RiskProfile?> GetByIdAsync(long id, CancellationToken ct = default)
            => await _db.RiskProfiles.FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<RiskProfile> CreateAsync(RiskProfile entity, CancellationToken ct = default)
        {
            var exists = await _db.RiskProfiles.AnyAsync(x => x.DocumentNo == entity.DocumentNo, cancellationToken: ct);

            if (exists)
                throw new ArgumentException("DocumentNo already exists");
                
            await _db.RiskProfiles.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
            return entity;
        }

        public async Task UpdateAsync(RiskProfile entity, CancellationToken ct = default)
        {
            _db.RiskProfiles.Update(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(RiskProfile entity, CancellationToken ct = default)
        {
            // Soft delete
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            await _db.SaveChangesAsync(ct);
        }

        public async Task<bool> ExistsAsync(long id, CancellationToken ct = default)
            => await _db.RiskProfiles.AnyAsync(p => p.Id == id, ct);
        }
}