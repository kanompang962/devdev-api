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
    public class CompanyRepo(AppDbContext _db) : ICompanyRepo
    {

        public async Task<(IReadOnlyList<Company> Items, int Total)> GetPagedAsync(int page, int pageSize, string? search, CancellationToken ct = default)
        {
            var query = _db.Companies.AsQueryable();

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

        public async Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken ct = default)
            => await _db.Companies.ToListAsync(ct);

        public async Task<Company?> GetByIdAsync(long id, CancellationToken ct = default)
            => await _db.Companies.FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<Company> CreateAsync(Company entity, CancellationToken ct = default)
        {
            var exists = await _db.Companies.AnyAsync(x => x.Code == entity.Code, cancellationToken: ct);

            if (exists)
                throw new ArgumentException("Company already exists");
                
            await _db.Companies.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}