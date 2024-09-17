using Microsoft.EntityFrameworkCore;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDatabaseContext _context;

    public ItemRepository(ApplicationDatabaseContext context)
    {
        _context = context ?? throw new ArgumentException("Context error", nameof(context));
    }

    public async Task<ItemEntity> GetByIdAsync(Guid id)
    {
        var entity = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        if (entity == null)
            throw new ArgumentException("Item with given Id is not found", nameof(id));
        
        return entity;
    }

    public async Task<IEnumerable<ItemEntity>> GetAllAsync()
    {
        var entities = await _context.Items.ToListAsync();
        if (entities == null || entities.Count == 0)
            throw new ArgumentException("No items found");
        
        return entities;
    }

    public async Task<IEnumerable<ItemEntity>> GetByFragileStatusAsync(bool isFragile)
    {
        var entities = await _context.Items
            .Where(i => i.IsFragile == isFragile).ToListAsync();
        if (entities == null || entities.Count == 0)
            throw new ArgumentException("No items found with the given fragile status", nameof(isFragile));
        
        return entities;
    }

    public async Task AddAsync(ItemEntity item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Item cannot be null");

        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ItemEntity item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Item cannot be null");

        var existingItem = await _context.Items.FindAsync(item.Id);
        if (existingItem == null)
            throw new ArgumentException("Item with given Id is not found", nameof(item.Id));

        _context.Entry(existingItem).CurrentValues.SetValues(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Items.FindAsync(id);
        if (entity == null)
            throw new ArgumentException("Item with given Id is not found", nameof(id));

        _context.Items.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
