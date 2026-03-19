using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace devdev_api.Data.Configurations.MasterConfigurations
{
    public class DepartmentConfig : BaseEntityConfig<Department>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("master_departments");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Description)
                .HasMaxLength(200);

            builder.HasIndex(e => e.Code).IsUnique();
            builder.HasIndex(e => e.FunctionalId);

            builder.HasOne(e => e.Functional)
                .WithMany(e => e.Departments)
                .HasForeignKey(e => e.FunctionalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}