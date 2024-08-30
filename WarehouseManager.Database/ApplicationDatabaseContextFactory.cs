using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WarehouseManager.Database;

public class ApplicationDatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDatabaseContext>
{
    public ApplicationDatabaseContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDatabaseContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseNpgsql(connectionString);
        

        return new ApplicationDatabaseContext(optionsBuilder.Options);
    }
}