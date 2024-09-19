using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.ContractsServices;

public interface IBossService
{
    Task<Boss> GetByIdAsync(Guid id);
    Task<Boss> GetByNameAsync(string name);
    Task<Boss> GetBySurnameAsync(string surname);
    Task<IEnumerable<Boss>> GetFilteredByDateOfRegAsync(DateTime dateOfReg);
    Task AddNewAsync(Boss boss);
    Task UpdateAsync(Boss boss);
    Task DeleteAsync(Guid id);
}