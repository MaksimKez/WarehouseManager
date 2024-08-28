using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.Database.Configurations;

public class TodoEntityConfiguration : IEntityTypeConfiguration<TodoEntity>
{
    public void Configure(EntityTypeBuilder<TodoEntity> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Description).HasMaxLength(500);
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.Deadline).IsRequired();
        builder.HasOne(t => t.Employee)
            .WithMany(e => e.Todos)
            .HasForeignKey(t => t.EmployeeId);
        builder.HasOne(t => t.Shelf)
            .WithMany()
            .HasForeignKey(t => t.ShelfId);
        builder.HasOne(t => t.Item)
            .WithMany()
            .HasForeignKey(t => t.ItemId);
    }
}