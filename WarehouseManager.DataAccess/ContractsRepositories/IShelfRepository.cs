using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.ContractsRepositories;

public interface IShelfRepository
{
    Task<ShelfEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<ShelfEntity>> GetAllAsync();
    Task<Guid> AddAsync(ShelfEntity shelf);
    Task UpdateAsync(ShelfEntity shelf);
    Task DeleteAsync(Guid id);
}
