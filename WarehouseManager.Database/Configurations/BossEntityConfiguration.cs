using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.Database.Configurations;

public class BossEntityConfiguration : IEntityTypeConfiguration<BossEntity>
{
    public void Configure(EntityTypeBuilder<BossEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
        builder.Property(b => b.Surname).IsRequired().HasMaxLength(50);
        builder.Property(b => b.Email).IsRequired().HasMaxLength(100);
        builder.Property(b => b.Password).IsRequired().HasMaxLength(100);
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}