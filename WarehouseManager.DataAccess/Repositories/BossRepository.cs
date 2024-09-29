using Microsoft.EntityFrameworkCore;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.Repositories;

public class BossRepository : IBossRepository
{
    private readonly ApplicationDatabaseContext _context;

    public BossRepository(ApplicationDatabaseContext context)
    {
        _context = context ?? throw new ArgumentException("Context error", nameof(context));
    }

    public async Task<BossEntity> GetByIdAsync(Guid id)
    {
        var entity = await _context.Bosses.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (entity == null)
            throw new ArgumentException("Boss with given Id is not found", nameof(id));
        
        return entity;
    }

    public async Task<BossEntity> GetByNameAsync(string name)
    {
        var entity = await _context.Bosses.AsNoTracking().FirstOrDefaultAsync(b => b.Name == name);
        if (entity == null)
            throw new ArgumentException("Boss with given name is not found", nameof(name));
        
        return entity;
    }

    public async Task<BossEntity> GetBySurnameAsync(string surname)
    {
        var entity = await _context.Bosses.AsNoTracking().FirstOrDefaultAsync(b => b.Surname == surname);
        if (entity == null)
            throw new ArgumentException("Boss with given surname is not found", nameof(surname));
        
        return entity;
    }

    public async Task<IEnumerable<BossEntity>> GetFilteredByDateOfRegAsync(DateTime dateOfReg)
    {
        var entities = await _context.Bosses.AsNoTracking()
            .Where(b => b.CreatedAt.Date == dateOfReg.Date).ToListAsync();
        if (entities == null || !entities.Any())
            throw new ArgumentException("No bosses found for the given registration date", nameof(dateOfReg));
        
        return entities;
    }

    public async Task<Guid> AddNewAsync(BossEntity boss)
    {
        if (boss == null)
            throw new ArgumentNullException(nameof(boss), "Boss cannot be null");

        await _context.Bosses.AddAsync(boss);
        await _context.SaveChangesAsync();

        return boss.Id;
    }

    public async Task UpdateAsync(BossEntity boss)
    {
        if (boss == null)
            throw new ArgumentNullException(nameof(boss), "Boss cannot be null");

        var existingBoss = await _context.Bosses.AsNoTracking().FirstOrDefaultAsync(b => b.Id == boss.Id);
        if (existingBoss == null)
            throw new ArgumentException("Boss with given Id is not found", nameof(boss.Id));

        _context.Entry(existingBoss).CurrentValues.SetValues(boss);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var boss = await _context.Bosses.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (boss == null)
            throw new ArgumentException("Boss with given Id is not found", nameof(id));

        _context.Bosses.Remove(boss);
        await _context.SaveChangesAsync();
    }
}
