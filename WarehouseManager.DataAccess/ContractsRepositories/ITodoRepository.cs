using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.ContractsRepositories;

public interface ITodoRepository
{
    Task<TodoEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<TodoEntity>> GetAllAsync();
    Task<IEnumerable<TodoEntity>> GetByEmployeeIdAsync(Guid employeeId);
    Task<IEnumerable<TodoEntity>> GetByShelfIdAsync(Guid shelfId);
    Task<IEnumerable<TodoEntity>> GetByItemIdAsync(Guid itemId);
    Task<IEnumerable<TodoEntity>> GetByIsDoneStatusAsync(bool isDone);
    Task<IEnumerable<TodoEntity>> GetByCreatedAtRangeAsync(DateTime startDate, DateTime endDate);
    Task AddAsync(TodoEntity todo);
    Task UpdateAsync(TodoEntity todo);
    Task DeleteAsync(Guid id);
}
