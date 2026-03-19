using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Data;
using devdev_api.Domain.Entities.Masters;
using devdev_api.Interfaces.Repositories.IMaster;
using Microsoft.EntityFrameworkCore;

namespace devdev_api.Repositories.MasterRepository
{
    public class YearRepo(AppDbContext _db) : IYearRepo
    {

        public async Task<(IReadOnlyList<Year> Items, int Total)> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default)
        {
            var query = _db.Years.AsQueryable();

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

        public async Task<IReadOnlyList<Year>> GetAllAsync(CancellationToken ct = default)
            => await _db.Years.ToListAsync(ct);

        public async Task<Year?> GetByIdAsync(long id, CancellationToken ct = default)
            => await _db.Years.FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<Year> CreateAsync(Year entity, CancellationToken ct = default)
        {
            var exists = await _db.Years.AnyAsync(x => x.YearValue == entity.YearValue, cancellationToken: ct);

            if (exists)
                throw new ArgumentException("Year already exists");
                
            await _db.Years.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}