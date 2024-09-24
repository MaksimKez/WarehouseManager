using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.ContractsServices;

public interface IEmployeeService
{
    Task Register(string name, string surname, string email, string password, PositionEnum position);
    Task<Employee> GetByIdAsync(Guid id);
    Task<Employee> GetByEmailAsync(string email);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<IEnumerable<Employee>> GetByPositionAsync(PositionEnum position);
    Task<IEnumerable<Employee>> GetBySurnameAsync(string surname);
    Task<IEnumerable<Employee>> GetByIsFiredStatusAsync(bool isFired);
    Task<IEnumerable<Employee>> GetByCreatedAtRangeAsync(DateTime startDate, DateTime endDate);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Guid id);
}