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
        var entity = await _context.Shelves.FirstOrDefaultAsync(s => s.Id == id);
        if (entity == null)
            throw new ArgumentException("Shelf with given Id is not found", nameof(id));
        
        return entity;
    }

    public async Task<IEnumerable<ShelfEntity>> GetAllAsync()
    {
        var entities = await _context.Shelves.ToListAsync();
        if (entities == null || entities.Count == 0)
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

        var existingShelf = await _context.Shelves.FindAsync(shelf.Id);
        if (existingShelf == null)
            throw new ArgumentException("Shelf with given Id is not found", nameof(shelf.Id));

        _context.Entry(existingShelf).CurrentValues.SetValues(shelf);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Shelves.FindAsync(id);
        if (entity == null)
            throw new ArgumentException("Shelf with given Id is not found", nameof(id));

        _context.Shelves.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
