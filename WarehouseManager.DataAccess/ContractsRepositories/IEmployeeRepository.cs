using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.ContractsRepositories;

public interface IEmployeeRepository
{
    Task<EmployeeEntity> GetByIdAsync(Guid id);
    Task<EmployeeEntity> GetByEmailAsync(string email);
    Task<IEnumerable<EmployeeEntity>> GetAllAsync();
    Task<IEnumerable<EmployeeEntity>> GetByPositionAsync(PositionEnum position);
    Task<IEnumerable<EmployeeEntity>> GetBySurnameAsync(string surname);
    Task<IEnumerable<EmployeeEntity>> GetByIsFiredStatusAsync(bool isFired);
    Task<IEnumerable<EmployeeEntity>> GetByCreatedAtRangeAsync(DateTime startDate, DateTime endDate);
    Task<Guid> AddAsync(EmployeeEntity employee);
    Task UpdateAsync(EmployeeEntity employee);
    Task DeleteAsync(Guid id);
}