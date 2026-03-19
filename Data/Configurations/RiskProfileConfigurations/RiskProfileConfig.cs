using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities.RiskProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace devdev_api.Data.Configurations.RiskProfileConfigurations
{
    public class RiskProfileConfig : BaseEntityConfig<RiskProfile>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<RiskProfile> builder)
        {
            builder.ToTable("risk_profiles");

            builder.Property(e => e.DocumentNo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.DocumentStatusId)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.CompanyId)
                .IsRequired();

            // ── Indexes ───────────────────────────────────────────────────────────
            builder.HasIndex(e => e.DocumentNo)
                .IsUnique()
                .HasFilter("is_deleted = false");

            builder.HasIndex(e => e.CompanyId);
            builder.HasIndex(e => e.DocumentStatusId);
            builder.HasIndex(e => new { e.CompanyId, e.DocumentStatusId, e.IsDeleted }); // composite
        }
    }
}