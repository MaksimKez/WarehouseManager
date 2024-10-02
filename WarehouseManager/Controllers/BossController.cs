using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.Dtos;
using WarehouseManager.Dtos.LoginDtos;
using WarehouseManager.Dtos.RegistrationDtos;

namespace WarehouseManager.Controllers;

//TODO add validation, hashing

public class BossController : ControllerBase
{
    private readonly IBossService _service;
    private readonly IMapper _mapper;


    public BossController(IBossService service, IMapper mapper)
    {
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
        _mapper = mapper ?? throw new ArgumentException("Mapper error", nameof(mapper));
    }

    [AllowAnonymous]
    [ProducesResponseType(typeof(BossRegistrationDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<BossRegistrationDto>> Register([FromBody]BossRegistrationDto bossRegistrationDto)
    {
        //add validation
        
        bossRegistrationDto.Id = await _service.Register(bossRegistrationDto.Name, bossRegistrationDto.Surname,
            bossRegistrationDto.Email, bossRegistrationDto.Password);

        return Created(nameof(GetById), bossRegistrationDto);
    }

    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> Login([FromBody]BossLoginDto bossLoginDto)
    {
        var token = await _service.Login(bossLoginDto.Name, bossLoginDto.Password);
        
        return Ok(token);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<BossDto>> GetById([FromBody] Guid id)
    {
        var boss = await _service.GetByIdAsync(id);
        var dto =  _mapper.Map<BossDto>(boss);

        return Ok(dto);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<BossDto>> GetByName([FromBody] string name)
    {
        var boss = await _service.GetByNameAsync(name);
        var dto =  _mapper.Map<BossDto>(boss);

        return Ok(dto);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<BossDto>> GetBySurname([FromBody] string surname)
    {
        var boss = await _service.GetBySurnameAsync(surname);
        var dto =  _mapper.Map<BossDto>(boss);

        return Ok(dto);
    }
    
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(BossDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<BossDto>> GetBySurname([FromBody] DateTime dateTimeReg)
    {
        var boss = await _service.GetFilteredByDateOfRegAsync(dateTimeReg);
        var dto =  _mapper.Map<BossDto>(boss);

        return Ok(dto);
    }
    
    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> AddNew([FromBody] BossDto dto)
    {
        var guid = await _service.AddNewAsync(_mapper.Map<Boss>(dto));
        
        return Created(nameof(GetById), guid);
    }

    [Authorize(Policy = "BossPolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update([FromBody] BossDto dto)
    {        
        await _service.UpdateAsync(_mapper.Map<Boss>(dto));
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