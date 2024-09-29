using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.ContractsServices;

public interface IBossService
{
    Task<Guid> Register(string name, string surname, string email, string password);
    Task<string> Login(string name, string password);
    Task<Boss> GetByIdAsync(Guid id);
    Task<Boss> GetByNameAsync(string name);
    Task<Boss> GetBySurnameAsync(string surname);
    Task<IEnumerable<Boss>> GetFilteredByDateOfRegAsync(DateTime dateOfReg);
    Task<Guid> AddNewAsync(Boss boss);
    Task UpdateAsync(Boss boss);
    Task DeleteAsync(Guid id);
}