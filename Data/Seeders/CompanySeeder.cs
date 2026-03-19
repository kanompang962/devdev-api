using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Masters;
using devdev_api.Interfaces.Services.IDataSeeders;
using Microsoft.EntityFrameworkCore;

namespace devdev_api.Data.Seeders
{
    public class CompanySeeder(AppDbContext db) : IDataSeeder
    {
        private readonly AppDbContext _db = db;

        public int Order => 2;

        public async Task SeedAsync()
        {
            var currentYear = await _db.Years
                .FirstOrDefaultAsync(y => y.IsCurrent);

            if (currentYear == null) return;

            // 👉 ถ้ามีแล้ว = ไม่ต้องทำ
            var hasCompany = await _db.Companies
                .AnyAsync(c => c.YearId == currentYear.Id);

            if (hasCompany) return;

            // 👉 หาปีเก่า
            var prevYear = await _db.Years
                .Where(y => y.YearValue < currentYear.YearValue)
                .OrderByDescending(y => y.YearValue)
                .FirstOrDefaultAsync();

            if (prevYear == null)
            {
                // 🔥 เคสนี้แหละที่คุณถาม → ไม่มีอะไรเลย
                var defaults = new[]
                {
                    new Company { Name = "TOP", Code = "TOP", YearId = currentYear.Id },
                    new Company { Name = "TPX", Code = "TPX", YearId = currentYear.Id },
                    new Company { Name = "TPL", Code = "TPL", YearId = currentYear.Id }
                };

                _db.Companies.AddRange(defaults);
            }
            else
            {
                // 👉 clone ปกติ
                var oldCompanies = await _db.Companies
                    .Where(c => c.YearId == prevYear.Id)
                    .ToListAsync();

                foreach (var old in oldCompanies)
                {
                    _db.Companies.Add(new Company
                    {
                        Name = old.Name,
                        Code = old.Code,
                        Description = old.Description,
                        YearId = currentYear.Id
                    });
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}