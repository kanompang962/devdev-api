var deletedItems = await _context.RiskProfiles
    .IgnoreQueryFilters() // สั่งให้หยุดกรอง IsDeleted = false
    .Where(x => x.IsDeleted)
    .ToListAsync();

dotnet ef migrations add Init 

dotnet ef database update   