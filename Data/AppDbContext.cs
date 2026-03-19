using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using devdev_api.Domain.Entities;
using devdev_api.Domain.Entities.Auth;
using devdev_api.Domain.Entities.Masters;
using devdev_api.Domain.Entities.RiskProfiles;
using devdev_api.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace devdev_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Auth
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        // User
        public DbSet<User>         Users         => Set<User>();
        public DbSet<Role>         Roles         => Set<Role>();
        public DbSet<UserRole>     UserRoles     => Set<UserRole>();

        // Master Data
        public DbSet<Year>       Years       => Set<Year>();
        public DbSet<Company>    Companies   => Set<Company>();
        public DbSet<Functional> Functionals => Set<Functional>();
        public DbSet<Department> Departments => Set<Department>();


        public DbSet<RiskProfile> RiskProfiles => Set<RiskProfile>();



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply all IEntityTypeConfiguration
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Global Soft Delete Filter
            // ApplySoftDeleteFilter(builder);
        }

        private static void ApplySoftDeleteFilter(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                    continue;

                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var prop = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                var body = Expression.Equal(prop, Expression.Constant(false));

                var lambda = Expression.Lambda(body, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            ApplyAudit();
            return base.SaveChangesAsync(ct);
        }

        private void ApplyAudit()
        {
            var now = DateTimeOffset.UtcNow;
            var user = "system";

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        entry.Entity.CreatedBy = user;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        entry.Entity.UpdatedBy = user;
                        break;

                    // case EntityState.Deleted:
                    //     entry.State = EntityState.Modified;
                    //     entry.Entity.IsDeleted = true;
                    //     entry.Entity.UpdatedAt = now;
                    //     entry.Entity.UpdatedBy = user;
                    //     break;
                }
            }
        }
    }
}