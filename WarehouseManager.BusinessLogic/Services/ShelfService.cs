using AutoMapper;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.Services;

public class ShelfService : IShelfService
{
    
    private readonly IShelfRepository _repository;
    private readonly IMapper _mapper;

    public ShelfService(IShelfRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentException("Repository error", nameof(repository));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
    }
    
    public async Task<Shelf> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new NullReferenceException("Shelf was null");

        return _mapper.Map<Shelf>(entity);
    }

    public async Task<IEnumerable<Shelf>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var shelves = entities.Select(en => _mapper.Map<Shelf>(en)).ToList();

        return shelves;
    }

    public async Task AddAsync(Shelf shelf)
    {
        await _repository.AddAsync(_mapper.Map<ShelfEntity>(shelf));
    }

    public async Task UpdateAsync(Shelf shelf)
    {
        await _repository.UpdateAsync(_mapper.Map<ShelfEntity>(shelf));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}