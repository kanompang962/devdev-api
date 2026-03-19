using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace devdev_api.Data.Configurations.UserConfigurations
{
    public class RoleConfig : BaseEntityConfig<Role>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(r => r.Name)
                .IsUnique();

            builder.HasData(
                new Role { Id = 1, Name = "Admin",   CreatedBy = "system" },
                new Role { Id = 2, Name = "Manager", CreatedBy = "system" },
                new Role { Id = 3, Name = "User",    CreatedBy = "system" }
            );
        }
    }
}