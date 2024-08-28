using WarehouseManager.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseManager.Database.Configurations;

public class EmployeeEntityConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Surname).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Password).IsRequired().HasMaxLength(100);
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.HasMany(e => e.Todos)
            .WithOne(t => t.Employee)
            .HasForeignKey(t => t.EmployeeId);
    }
}
