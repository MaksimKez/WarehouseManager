using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database.Entities;
using WarehouseManager.Dtos;
using WarehouseManager.Dtos.LoginDtos;
using WarehouseManager.Dtos.RegistrationDtos;

namespace WarehouseManager.Controllers;

public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;
    private readonly IMapper _mapper;
    
    public EmployeeController(IMapper mapper, IEmployeeService service)
    {
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
    }

    [AllowAnonymous]
    [ProducesResponseType(typeof(EmployeeRegistrationDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<EmployeeRegistrationDto>> Register([FromBody] EmployeeRegistrationDto employeeRegistrationDto)
    {
        employeeRegistrationDto.Id = await _service.Register(employeeRegistrationDto.Name,
            employeeRegistrationDto.Surname, employeeRegistrationDto.Email, employeeRegistrationDto.Password,
            employeeRegistrationDto.Position);
        employeeRegistrationDto.IsFired = false;
        
        return Created(nameof(GetById), employeeRegistrationDto);
    }

    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> Login([FromBody] EmployeeLoginDto employeeLoginDto)
    {
        var token = await _service.Login(employeeLoginDto.Email, employeeLoginDto.Password);
        
        return Ok(token);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeDto>> GetById([FromBody] Guid id)
    {
        var dto = _mapper.Map<EmployeeDto>(await _service.GetByIdAsync(id));
        
        return Ok(dto);
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeDto>> GetByEmail([FromBody] string email)
    {
        var dto = _mapper.Map<EmployeeDto>(await _service.GetByEmailAsync(email));
        return Ok(dto);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetAll()
    {
        var employees = await _service.GetAllAsync();
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        
        return Ok(dtos);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetByPosition([FromBody] PositionEnum position)
    {
        var employees = await _service.GetByPositionAsync(position);
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        
        return Ok(dtos);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetBySurname([FromBody] string surname)
    {
        var employees = await _service.GetBySurnameAsync(surname);
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        
        return Ok(dtos);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetByIsFiredStatus([FromBody] bool isFired)
    {
        var employees = await _service.GetByIsFiredStatusAsync(isFired);
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        
        return Ok(dtos);
    }
    
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetByCreatedAtRange([FromBody] DateTime startDate,
        [FromBody] DateTime endDate)
    {
        var employees = await _service.GetByCreatedAtRangeAsync(startDate, endDate);
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        
        return Ok(dtos);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> Add([FromBody] EmployeeDto dto)
    {
        return Created(nameof(GetById) ,await _service.AddAsync(_mapper.Map<Employee>(dto)));
    }
    
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update([FromBody] EmployeeDto dto)
    {
        await _service.UpdateAsync(_mapper.Map<Employee>(dto));
        return Ok();
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete([FromBody] Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}