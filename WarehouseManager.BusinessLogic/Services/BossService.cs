using AutoMapper;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.Services;

public class BossService : IBossService
{
    private readonly IBossRepository _repository;
    private readonly IMapper _mapper;

    public BossService(IBossRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentException("Repository error", nameof(repository));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
    }

    public async Task<Boss> GetByIdAsync(Guid id)
    { 
        var boss = _mapper.Map<BossEntity, Boss>(await _repository.GetByIdAsync(id));
        return boss ?? throw new NullReferenceException("Boss was null");
    }

    public async Task<Boss> GetByNameAsync(string name)
    {
        var boss = _mapper.Map<BossEntity, Boss>(await _repository.GetByNameAsync(name));
        return boss ?? throw new NullReferenceException("Boss was null");
    }

    public async Task<Boss> GetBySurnameAsync(string surname)
    {
        var boss = _mapper.Map<BossEntity, Boss>(await _repository.GetBySurnameAsync(surname));
        return boss ?? throw new NullReferenceException("Boss was null");
    }

    public async Task<IEnumerable<Boss>> GetFilteredByDateOfRegAsync(DateTime dateOfReg)
    {
        var bossEntities = await _repository.GetFilteredByDateOfRegAsync(dateOfReg);
        //todo BENCHMARK
        //var bosses = bossEntities.Select(en => _mapper.Map<Boss>(en)).ToList();
        //var bosses = await Task.WhenAll(bossEntities.Select(async en => await Task.Run(() => _mapper.Map<Boss>(en))));
        //var bossCollection = bosses.AsEnumerable();
        //return bossCollection;
        return null;
    }

    public Task AddNewAsync(Boss boss)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Boss boss)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}