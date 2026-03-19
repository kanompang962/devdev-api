var deletedItems = await _context.RiskProfiles
    .IgnoreQueryFilters() // สั่งให้หยุดกรอง IsDeleted = false
    .Where(x => x.IsDeleted)
    .ToListAsync();

dotnet ef database drop -f
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
