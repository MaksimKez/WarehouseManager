using AutoMapper;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public ItemService(IItemRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentException("Repository error", nameof(repository));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
    }

    public async Task<Item> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
            throw new NullReferenceException("Item was null");
        
        return _mapper.Map<Item>(item);
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var items = entities.Select(en => _mapper.Map<Item>(en)).ToList();
        return items;
    }

    public async Task<IEnumerable<Item>> GetByFragileStatusAsync(bool isFragile)
    {
        var entities = await _repository.GetByFragileStatusAsync(isFragile);
        var items = entities.Select(en => _mapper.Map<Item>(en));
        return items;
    }

    public async Task<Guid> AddAsync(Item item)
    {
        return await _repository.AddAsync(_mapper.Map<ItemEntity>(item));
    }

    public async Task UpdateAsync(Item item)
    {
        await _repository.UpdateAsync(_mapper.Map<ItemEntity>(item));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}