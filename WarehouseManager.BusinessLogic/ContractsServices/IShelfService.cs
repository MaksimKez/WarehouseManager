using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.ContractsServices;

public interface IShelfService
{
    Task<Shelf> GetByIdAsync(Guid id);
    Task<IEnumerable<Shelf>> GetAllAsync();
    Task<Guid> AddAsync(Shelf shelf);
    Task UpdateAsync(Shelf shelf);
    Task DeleteAsync(Guid id);
}