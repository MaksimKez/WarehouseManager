using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.Database.Configurations;

public class ItemEntityConfiguration : IEntityTypeConfiguration<ItemEntity>
{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Description).IsRequired().HasMaxLength(200);
        builder.Property(i => i.IsFragile).IsRequired();
    }
}