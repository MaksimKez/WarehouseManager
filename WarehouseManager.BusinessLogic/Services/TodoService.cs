using AutoMapper;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public TodoService(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentException("Repository error", nameof(repository));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
    }
    
    public async Task<Todo> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new NullReferenceException("Shelf was null");

        return _mapper.Map<Todo>(entity);
    }

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var todos = entities.Select(en => _mapper.Map<Todo>(en)).ToList();

        return todos;
    }

    public async Task<IEnumerable<Todo>> GetByEmployeeIdAsync(Guid employeeId)
    {
        var entities = await _repository.GetByEmployeeIdAsync(employeeId);
        var todos = entities.Select(en => _mapper.Map<Todo>(en)).ToList();

        return todos;
    }

    public async Task<IEnumerable<Todo>> GetByShelfIdAsync(Guid shelfId)
    {
        var entities = await _repository.GetByShelfIdAsync(shelfId);
        var todos = entities.Select(en => _mapper.Map<Todo>(en)).ToList();

        return todos;
    }

    public async Task<IEnumerable<Todo>> GetByItemIdAsync(Guid itemId)
    {
        var entities = await _repository.GetByItemIdAsync(itemId);
        var todos = entities.Select(en => _mapper.Map<Todo>(en)).ToList();

        return todos;
    }

    public async Task<IEnumerable<Todo>> GetByIsDoneStatusAsync(bool isDone)
    {
        var entities = await _repository.GetByIsDoneStatusAsync(isDone);
        var todos = entities.Select(en => _mapper.Map<Todo>(en)).ToList();

        return todos;
    }

    public async Task<IEnumerable<Todo>> GetByCreatedAtRangeAsync(DateTime startDate, DateTime endDate)
    {
        var entities = await _repository.GetByCreatedAtRangeAsync(startDate, endDate);
        var todos = entities.Select(en => _mapper.Map<Todo>(en)).ToList();

        return todos;
    }

    public async Task AddAsync(Todo todo)
    {
        await _repository.AddAsync(_mapper.Map<TodoEntity>(todo));
    }

    public async Task UpdateAsync(Todo todo)
    {
        await _repository.UpdateAsync(_mapper.Map<TodoEntity>(todo));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}