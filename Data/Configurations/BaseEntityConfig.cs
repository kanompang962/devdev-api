using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devdev_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace devdev_api.Data.Configurations
{
    public abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            // ใช้ Identity Column ตามมาตรฐาน Postgres 10+
            builder.Property(e => e.Id)
                .UseIdentityByDefaultColumn();

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.Property(e => e.UpdatedAt);

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.UpdatedBy)
                .HasMaxLength(100);

            builder.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // ✅ Index เพื่อ performance ของ soft-delete query
            builder.HasIndex(e => e.IsDeleted);
            builder.HasIndex(e => e.IsActive);

            // ให้ subclass configure ต่อได้
            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}