using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.ContractsServices;

public interface ITodoService
{
    Task<Todo> GetByIdAsync(Guid id);
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<IEnumerable<Todo>> GetByEmployeeIdAsync(Guid employeeId);
    Task<IEnumerable<Todo>> GetByShelfIdAsync(Guid shelfId);
    Task<IEnumerable<Todo>> GetByItemIdAsync(Guid itemId);
    Task<IEnumerable<Todo>> GetByIsDoneStatusAsync(bool isDone);
    Task<IEnumerable<Todo>> GetByCreatedAtRangeAsync(DateTime startDate, DateTime endDate);
    Task AddAsync(Todo todo);
    Task UpdateAsync(Todo todo);
    Task DeleteAsync(Guid id);
}