using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace devdev_api.Data.Configurations.MasterConfigurations
{
    public class CompanyConfig : BaseEntityConfig<Company>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("master_companies");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Description)
                .HasMaxLength(200);

            builder.HasIndex(e => e.Code).IsUnique();
            builder.HasIndex(e => e.YearId);

            builder.HasOne(e => e.Year)
                .WithMany(e => e.Companies)
                .HasForeignKey(e => e.YearId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Functionals)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}