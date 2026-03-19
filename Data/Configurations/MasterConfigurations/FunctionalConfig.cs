using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace devdev_api.Data.Configurations.MasterConfigurations
{
    public class FunctionalConfig : BaseEntityConfig<Functional>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Functional> builder)
        {
            builder.ToTable("master_functionals");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Description)
                .HasMaxLength(200);

            builder.HasIndex(e => e.Code).IsUnique();
            builder.HasIndex(e => e.CompanyId);

            builder.HasOne(e => e.Company)
                .WithMany(e => e.Functionals)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
