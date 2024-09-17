using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.ContractsRepositories;

public interface IBossRepository
{
    Task<BossEntity> GetByIdAsync(Guid id);
    Task<BossEntity> GetByNameAsync(string name);
    Task<BossEntity> GetBySurnameAsync(string surname);
    Task<IEnumerable<BossEntity>> GetFilteredByDateOfRegAsync(DateTime dateOfReg);
    Task AddNewAsync(BossEntity boss);
    Task UpdateAsync(BossEntity boss);
    Task DeleteAsync(Guid id);
}