using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.ContractsServices;

public interface IItemService
{
    Task<Item> GetByIdAsync(Guid id);
    Task<IEnumerable<Item>> GetAllAsync();
    Task<IEnumerable<Item>> GetByFragileStatusAsync(bool isFragile);
    Task AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(Guid id);
}