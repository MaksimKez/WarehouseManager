using WarehouseManager.Services.Models;

namespace WarehouseManager.Services.ContractsServices;

public interface IItemService
{
    Task<Item> GetByIdAsync(Guid id);
    Task<IEnumerable<Item>> GetAllAsync();
    Task<IEnumerable<Item>> GetByFragileStatusAsync(bool isFragile);
    Task AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(Guid id);
}