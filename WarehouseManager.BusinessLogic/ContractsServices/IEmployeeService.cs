using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.ContractsServices;

public interface IEmployeeService
{
    Task<Guid> Register(string name, string surname, string email, string password, PositionEnum position);
    Task<string> Login(string email, string password);
    Task<Employee> GetByIdAsync(Guid id);
    Task<Employee> GetByEmailAsync(string email);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<IEnumerable<Employee>> GetByPositionAsync(PositionEnum position);
    Task<IEnumerable<Employee>> GetBySurnameAsync(string surname);
    Task<IEnumerable<Employee>> GetByIsFiredStatusAsync(bool isFired);
    Task<IEnumerable<Employee>> GetByCreatedAtRangeAsync(DateTime startDate, DateTime endDate);
    Task<Guid> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Guid id);
}