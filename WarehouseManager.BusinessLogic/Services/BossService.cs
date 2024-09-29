using AutoMapper;
using WarehouseManager.BusinessLogic.Auth.Interfaces;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Exceptions;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.Services;

public class BossService : IBossService
{
    private readonly IBossRepository _repository;
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;

    public BossService(IBossRepository repository, IMapper mapper, IJwtProvider jwtProvider)
    {
        _repository = repository ?? throw new ArgumentException("Repository error", nameof(repository));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
        _jwtProvider = jwtProvider ?? throw new ArgumentException("JwtProvider error", nameof(mapper));
    }
    
    public async Task<Guid> Register(string name, string surname, string email,
        string password)
    {
        var boss = new Boss()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Surname = surname,
            Email = email,
            CreatedAt = DateTime.Now,
            Password = password,
        };

        return await _repository.AddNewAsync(_mapper.Map<BossEntity>(boss));
    }

    public async Task<string> Login(string name, string password)
    {
        // validate data
        var boss = _mapper.Map<Boss>(await _repository.GetByNameAsync(name));

        if (!password.Equals(boss.Password))
        {
            throw new InvalidPasswordException();
        }
        
        var token = _jwtProvider.GenerateToken(boss);
        return token;
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
        var bosses = bossEntities.Select(en => _mapper.Map<Boss>(en)).ToList();
        return bosses;
    }

    public async Task<Guid> AddNewAsync(Boss boss)
    {
        return await _repository.AddNewAsync(_mapper.Map<BossEntity>(boss));
    }

    public async Task UpdateAsync(Boss boss)
    {
        await _repository.UpdateAsync(_mapper.Map<BossEntity>(boss));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}