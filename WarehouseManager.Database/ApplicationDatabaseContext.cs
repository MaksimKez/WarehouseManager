using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.Database;

public class ApplicationDatabaseContext : DbContext
{
    public DbSet<BossEntity> Bosses { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<TodoEntity> Todos { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<ShelfEntity> Shelves { get; set; }
    
    public ApplicationDatabaseContext() { }

    public ApplicationDatabaseContext(DbContextOptions options)
        : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("WarehouseManager"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}