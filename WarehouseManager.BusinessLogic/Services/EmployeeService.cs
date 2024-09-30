using AutoMapper;
using WarehouseManager.BusinessLogic.Auth.Interfaces;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Exceptions;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database.Entities;

namespace WarehouseManager.BusinessLogic.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;

    public EmployeeService(IMapper mapper, IEmployeeRepository repository, IJwtProvider provider)
    {
        _repository = repository ?? throw new ArgumentException("Repository error", nameof(repository));
        _jwtProvider = provider?? throw new ArgumentException("JwtProvider error", nameof(mapper));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
    }

    public async Task<Guid> Register(string name, string surname, string email,
        string password, PositionEnum position)
    {
        var employee = new Employee()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Surname = surname,
            Email = email,
            CreatedAt = DateTime.Now,
            IsFired = false,
            Password = password,
            Position = position
        };

        return await _repository.AddAsync(_mapper.Map<EmployeeEntity>(employee));
    }

    public async Task<string> Login(string email, string password)
    {
        // validate data
        var employee = _mapper.Map<Employee>(await _repository.GetByEmailAsync(email));

        if (!password.Equals(employee.Password))
        {
            throw new InvalidPasswordException();
        }
        
        var token = _jwtProvider.GenerateToken(employee);
        return token;
    }
    
    public async Task<Employee> GetByIdAsync(Guid id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
            throw new NullReferenceException("Employee was null");
        
        return _mapper.Map<Employee>(employee);
    }

    public async Task<Employee> GetByEmailAsync(string email)
    {
        var employee = await _repository.GetByEmailAsync(email);
        if (employee == null)
            throw new NullReferenceException("Employee was null");
        
        return _mapper.Map<Employee>(employee);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        var employees = entities.Select(en => _mapper.Map<Employee>(en)).ToList();
        return employees;
    }

    public async Task<IEnumerable<Employee>> GetByPositionAsync(PositionEnum position)
    {
        var entities = await _repository.GetByPositionAsync(position);
        var filtered = entities.Select(en => _mapper.Map<Employee>(en)).ToList();
        return filtered;
    }

    public async Task<IEnumerable<Employee>> GetBySurnameAsync(string surname)
    {
        var entities = await _repository.GetBySurnameAsync(surname);
        var employees = entities.Select(en => _mapper.Map<Employee>(en)).ToList();
        return employees;
    }

    public async Task<IEnumerable<Employee>> GetByIsFiredStatusAsync(bool isFired)
    {
        var entities = await _repository.GetByIsFiredStatusAsync(isFired);
        var employees = entities.Select(en => _mapper.Map<Employee>(en)).ToList();
        return employees;
    }

    public async Task<IEnumerable<Employee>> GetByCreatedAtRangeAsync(DateTime startDate, DateTime endDate)
    {
        var entities = await _repository.GetByCreatedAtRangeAsync(startDate, endDate);
        var employees = entities.Select(en => _mapper.Map<Employee>(en)).ToList();
        return employees;
    }

    public async Task<Guid> AddAsync(Employee employee)
    {
        return await _repository.AddAsync(_mapper.Map<EmployeeEntity>(employee));
    }

    public async Task UpdateAsync(Employee employee)
    {
        await _repository.UpdateAsync(_mapper.Map<EmployeeEntity>(employee));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}