using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        var entity = await _context.Bosses.FirstOrDefaultAsync(b => b.Id == id);
        if (entity == null)
            throw new ArgumentException("Boss with given Id is not found", nameof(id));
        
        return entity;
    }
    public async Task<BossEntity> GetByNameAsync(string name)
    {
        var entity = await _context.Bosses.FirstOrDefaultAsync(b => b.Name == name);
        if (entity == null)
            throw new ArgumentException("Boss with given name is not found", nameof(name));
        
        return entity;
    }

    public async Task<IEnumerable<BossEntity>> GetFilteredByDateOfRegAsync(DateTime dateOfReg)
    {
        return await _context.Bosses.Where(b => b.CreatedAt.Date == dateOfReg.Date).ToListAsync();
    }

    public async Task AddNewAsync(BossEntity boss)
    {
        await _context.Bosses.AddAsync(boss);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BossEntity boss)
    {
        var existingBoss = await _context.Bosses.FindAsync(boss.Id);
        if (existingBoss == null)
            throw new ArgumentException("Boss with given Id is not found", nameof(boss.Id));

        _context.Entry(existingBoss).CurrentValues.SetValues(boss);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var boss = await _context.Bosses.FindAsync(id);
        if (boss == null)
            throw new ArgumentException("Boss with given Id is not found", nameof(id));

        _context.Bosses.Remove(boss);
        await _context.SaveChangesAsync();
    }
}