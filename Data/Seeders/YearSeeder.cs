using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Masters;
using devdev_api.Interfaces.Services.IDataSeeders;
using Microsoft.EntityFrameworkCore;

namespace devdev_api.Data.Seeders
{
    public class YearSeeder(AppDbContext db) : IDataSeeder
    {
        private readonly AppDbContext _db = db;

        public int Order => 1;

        public async Task SeedAsync()
        {
            var currentYear = DateTime.UtcNow.Year;

            // สร้างปีปัจจุบัน + อีก 2 ปี
            for (int year = currentYear; year <= currentYear + 2; year++)
            {
                var exists = await _db.Years.AnyAsync(y => y.YearValue == year);
                if (!exists)
                {
                    _db.Years.Add(new Year
                    {
                        YearValue = year,
                        IsCurrent = year == currentYear
                    });
                }
            }

            // update IsCurrent สำหรับปีอื่น
            var allYears = await _db.Years.ToListAsync();
            foreach (var y in allYears)
            {
                y.IsCurrent = y.YearValue == currentYear;
            }

            await _db.SaveChangesAsync();
        }
    }
}