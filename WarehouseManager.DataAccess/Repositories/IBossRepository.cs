using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.Repositories;

public interface IBossRepository
{
    Task<BossEntity> GetByIdAsync(Guid id);
    Task<BossEntity> GetByNameAsync(string name);
    Task<IEnumerable<BossEntity>> GetFilteredByDateOfRegAsync(DateTime dateOfReg);
    Task AddNewAsync(BossEntity boss);
    Task UpdateAsync(BossEntity boss);
    Task DeleteAsync(int id);
}