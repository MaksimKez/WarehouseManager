using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.BusinessLogic.Validators;
using WarehouseManager.DataAccess.ContractsRepositories;
using WarehouseManager.Database.Entities;
using WarehouseManager.Dtos;
using WarehouseManager.Dtos.LoginDtos;
using WarehouseManager.Dtos.RegistrationDtos;

namespace WarehouseManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;
    private readonly IMapper _mapper;
    private readonly EmployeeValidator _employeeValidator;
    
    public EmployeeController(IMapper mapper, IEmployeeService service, EmployeeValidator employeeValidator)
    {
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
        _employeeValidator = employeeValidator ?? throw new ArgumentException("Validator error", nameof(employeeValidator));
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EmployeeRegistrationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeRegistrationDto>> Register([FromBody] EmployeeRegistrationDto employeeRegistrationDto)
    {
        employeeRegistrationDto.Id = await _service.Register(employeeRegistrationDto.Name,
            employeeRegistrationDto.Surname, employeeRegistrationDto.Email, employeeRegistrationDto.Password,
            employeeRegistrationDto.Position);
        employeeRegistrationDto.IsFired = false;
        
        return Created(nameof(GetById), employeeRegistrationDto);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> Login([FromBody] EmployeeLoginDto employeeLoginDto)
    {
        var token = await _service.Login(employeeLoginDto.Email, employeeLoginDto.Password);
        return Ok(token);
    }

    [HttpGet("getById/{id}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id)
    {
        var dto = _mapper.Map<EmployeeDto>(await _service.GetByIdAsync(id));
        return Ok(dto);
    }

    [HttpGet("getByEmail/{email}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeDto>> GetByEmail(string email)
    {
        var dto = _mapper.Map<EmployeeDto>(await _service.GetByEmailAsync(email));
        return Ok(dto);
    }

    [HttpGet("getAll")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetAll()
    {
        var employees = await _service.GetAllAsync();
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        return Ok(dtos);
    }

    [HttpGet("getByPosition/{position}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(List<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetByPosition(PositionEnum position)
    {
        var employees = await _service.GetByPositionAsync(position);
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        return Ok(dtos);
    }

    [HttpGet("getBySurname/{surname}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(List<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetBySurname(string surname)
    {
        var employees = await _service.GetBySurnameAsync(surname);
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        return Ok(dtos);
    }

    [HttpGet("getByIsFiredStatus/{isFired}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(List<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetByIsFiredStatus(bool isFired)
    {
        var employees = await _service.GetByIsFiredStatusAsync(isFired);
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        return Ok(dtos);
    }
    
    [HttpGet("getByCreatedAtRange")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(List<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeDto>>> GetByCreatedAtRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var employees = await _service.GetByCreatedAtRangeAsync(startDate, endDate);
        var dtos = employees.Select(em => _mapper.Map<EmployeeDto>(em)).ToList();
        return Ok(dtos);
    }

    [HttpPost("add")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Add([FromBody] EmployeeDto dto)
    {
        var model = _mapper.Map<Employee>(dto);
        var validationResult = await _employeeValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var id = await _service.AddAsync(_mapper.Map<Employee>(dto));
        return Created(nameof(GetById), id);
    }
    
    [HttpPut("update")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update([FromBody] EmployeeDto dto)
    {
        var model = _mapper.Map<Employee>(dto);
        var validationResult = await _employeeValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _service.UpdateAsync(_mapper.Map<Employee>(dto));
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
