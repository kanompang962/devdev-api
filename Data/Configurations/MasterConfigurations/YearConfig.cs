using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace devdev_api.Data.Configurations.MasterConfigurations
{
    public class YearConfig : BaseEntityConfig<Year>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Year> builder)
        {
            builder.ToTable("master_years");

            builder.Property(e => e.YearValue)
                .IsRequired();

            builder.Property(e => e.IsCurrent)
                .HasDefaultValue(false);

            builder.HasIndex(e => e.YearValue).IsUnique();
            builder.HasIndex(e => e.IsCurrent);
        }
    }
}