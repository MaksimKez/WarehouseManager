using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.BusinessLogic.Validators;
using WarehouseManager.Dtos;
using WarehouseManager.Dtos.LoginDtos;
using WarehouseManager.Dtos.RegistrationDtos;

namespace WarehouseManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BossController : ControllerBase
{
    private readonly IBossService _service;
    private readonly IMapper _mapper;
    private readonly BossValidator _bossValidator;
    
    public BossController(IBossService service, IMapper mapper, BossValidator bossValidator)
    {
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
        _bossValidator = bossValidator ?? throw new ArgumentException("BossValidator error", nameof(bossValidator));
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BossRegistrationDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<BossRegistrationDto>> Register([FromBody] BossRegistrationDto bossRegistrationDto)
    {
        bossRegistrationDto.Id = await _service.Register(bossRegistrationDto.Name, bossRegistrationDto.Surname,
            bossRegistrationDto.Email, bossRegistrationDto.Password);

        return Created(nameof(GetById), bossRegistrationDto);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> Login([FromBody] BossLoginDto bossLoginDto)
    {
        var token = await _service.Login(bossLoginDto.Name, bossLoginDto.Password);
        
        return Ok(token);
    }

    [HttpGet("getById/{id}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<BossDto>> GetById(Guid id)
    {
        var boss = await _service.GetByIdAsync(id);
        var dto = _mapper.Map<BossDto>(boss);

        if (dto == null)
        {
            return NotFound();
        }

        return Ok(dto);
    }

    [HttpGet("getByName/{name}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<BossDto>> GetByName(string name)
    {
        var boss = await _service.GetByNameAsync(name);
        var dto = _mapper.Map<BossDto>(boss);
        
        if (dto == null)
        {
            return NotFound();
        }

        return Ok(dto);
    }

    [HttpGet("getBySurname/{surname}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<BossDto>> GetBySurname(string surname)
    {
        var boss = await _service.GetBySurnameAsync(surname);
        var dto = _mapper.Map<BossDto>(boss);
        
        if (dto == null)
        {
            return NotFound();
        }

        return Ok(dto);
    }
    
    [HttpGet("getByRegistrationDate/{dateTimeReg}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<BossDto>> GetByDateOfRegistration(DateTime dateTimeReg)
    {
        var boss = await _service.GetFilteredByDateOfRegAsync(dateTimeReg);
        var dto = _mapper.Map<BossDto>(boss);

        return Ok(dto);
    }
    
    [HttpPost("add")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)] 
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> AddNew([FromBody] Boss boss)
    {
        var validationResult = await _bossValidator.ValidateAsync(boss);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var guid = await _service.AddNewAsync(boss);
        
        return Created(nameof(GetById), guid);
    }

    [HttpPut("update")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update([FromBody] Boss boss)
    {
        var validationResult = await _bossValidator.ValidateAsync(boss);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        await _service.UpdateAsync(boss);
        return Ok();
    }
    
    // Delete Boss by Id
    [HttpDelete("delete/{id}")]
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
