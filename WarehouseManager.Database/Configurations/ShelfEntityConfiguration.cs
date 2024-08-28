using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.Database.Configurations;

public class ShelfEntityConfiguration : IEntityTypeConfiguration<ShelfEntity>
{
    public void Configure(EntityTypeBuilder<ShelfEntity> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Description).IsRequired().HasMaxLength(200);
    }
}