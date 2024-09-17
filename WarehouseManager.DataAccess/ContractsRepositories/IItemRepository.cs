using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.ContractsRepositories;

public interface IItemRepository
{
    Task<ItemEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<ItemEntity>> GetAllAsync();
    Task<IEnumerable<ItemEntity>> GetByFragileStatusAsync(bool isFragile);
    Task AddAsync(ItemEntity item);
    Task UpdateAsync(ItemEntity item);
    Task DeleteAsync(Guid id);
}