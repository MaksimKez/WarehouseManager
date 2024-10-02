using Microsoft.EntityFrameworkCore;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.DataAccess.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDatabaseContext _context;

    public TodoRepository(ApplicationDatabaseContext context)
    {
        _context = context ?? throw new ArgumentException("Context error", nameof(context));
    }

    public async Task<TodoEntity> GetByIdAsync(Guid id)
    {
        var entity = await _context.Todos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        if (entity == null)
            throw new ArgumentException("Todo with given Id is not found", nameof(id));
        
        return entity;
    }

    public async Task<IEnumerable<TodoEntity>> GetAllAsync()
    {
        var entities = await _context.Todos.AsNoTracking().ToListAsync();
        if (entities == null || entities.Count == 0)
            throw new ArgumentException("No todos found");
        
        return entities;
    }

    public async Task<IEnumerable<TodoEntity>> GetByEmployeeIdAsync(Guid employeeId)
    {
        var entities = await _context.Todos
            .Where(t => t.EmployeeId == employeeId).AsNoTracking().ToListAsync();
        if (entities == null || !entities.Any())
            throw new ArgumentException("No todos found for the given employee", nameof(employeeId));
        
        return entities;
    }

    public async Task<IEnumerable<TodoEntity>> GetByShelfIdAsync(Guid shelfId)
    {
        var entities = await _context.Todos.AsNoTracking()
            .Where(t => t.ShelfId == shelfId).ToListAsync();
        if (entities == null || !entities.Any())
            throw new ArgumentException("No todos found for the given shelf", nameof(shelfId));
        
        return entities;
    }

    public async Task<IEnumerable<TodoEntity>> GetByItemIdAsync(Guid itemId)
    {
        var entities = await _context.Todos.AsNoTracking()
            .Where(t => t.ItemId == itemId).ToListAsync();
        if (entities == null || !entities.Any())
            throw new ArgumentException("No todos found for the given item", nameof(itemId));
        
        return entities;
    }

    public async Task<IEnumerable<TodoEntity>> GetByIsDoneStatusAsync(bool isDone)
    {
        var entities = await _context.Todos.AsNoTracking()
            .Where(t => t.IsDone == isDone).ToListAsync();
        if (entities == null || !entities.Any())
            throw new ArgumentException("No todos found with the given status", nameof(isDone));
        
        return entities;
    }

    public async Task<IEnumerable<TodoEntity>> GetByCreatedAtRangeAsync(DateTime startDate, DateTime endDate)
    {
        var entities = await _context.Todos.AsNoTracking()
            .Where(t => t.CreatedAt >= startDate && t.CreatedAt <= endDate).ToListAsync();
        if (entities == null || !entities.Any())
            throw new ArgumentException("No todos found in the given date range", nameof(startDate));
        
        return entities;
    }

    public async Task<Guid> AddAsync(TodoEntity todo)
    {
        if (todo == null)
            throw new ArgumentNullException(nameof(todo), "Todo cannot be null");

        await _context.Todos.AddAsync(todo);
        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task UpdateAsync(TodoEntity todo)
    {
        if (todo == null)
            throw new ArgumentNullException(nameof(todo), "Todo cannot be null");

        var existingTodo = await _context.Todos.
            AsNoTracking().FirstOrDefaultAsync(b => b.Id == todo.Id);
        if (existingTodo == null)
            throw new ArgumentException("Todo with given Id is not found", nameof(todo.Id));

        _context.Entry(existingTodo).CurrentValues.SetValues(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var todo = await _context.Todos.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (todo == null)
            throw new ArgumentException("Shelf with given Id is not found", nameof(id));

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }
}
