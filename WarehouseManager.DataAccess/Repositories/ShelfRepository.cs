using Microsoft.EntityFrameworkCore;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.Repositories;

public class ShelfRepository : IShelfRepository
{
    private readonly ApplicationDatabaseContext _context;

    public ShelfRepository(ApplicationDatabaseContext context)
    {
        _context = context ?? throw new ArgumentException("Context error", nameof(context));
    }

    public async Task<ShelfEntity> GetByIdAsync(Guid id)
    {
        var entity = await _context.Shelves.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (entity == null)
            throw new ArgumentException("Shelf with given Id is not found", nameof(id));
        
        return entity;
    }

    public async Task<IEnumerable<ShelfEntity>> GetAllAsync()
    {
        var entities = await _context.Shelves.ToListAsync();
        if (entities == null || !entities.Any())
            throw new ArgumentException("No shelves found");
        
        return entities;
    }

    public async Task AddAsync(ShelfEntity shelf)
    {
        if (shelf == null)
            throw new ArgumentNullException(nameof(shelf), "Shelf cannot be null");

        await _context.Shelves.AddAsync(shelf);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ShelfEntity shelf)
    {
        if (shelf == null)
            throw new ArgumentNullException(nameof(shelf), "Shelf cannot be null");

        var existingShelf = await _context.Shelves.
            AsNoTracking().FirstOrDefaultAsync(b => b.Id == shelf.Id);
        if (existingShelf == null)
            throw new ArgumentException("Shelf with given Id is not found", nameof(shelf.Id));

        _context.Entry(existingShelf).CurrentValues.SetValues(shelf);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var shelf = await _context.Shelves.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (shelf == null)
            throw new ArgumentException("Shelf with given Id is not found", nameof(id));

        _context.Shelves.Remove(shelf);
        await _context.SaveChangesAsync();
    }
}
