using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
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

    public async Task<ActionResult<BossRegistrationDto>> Register([FromBody]BossRegistrationDto bossRegistrationDto)
    {
        //add validation
        
        bossRegistrationDto.Id = await _service.Register(bossRegistrationDto.Name, bossRegistrationDto.Surname,
            bossRegistrationDto.Email, bossRegistrationDto.Password);

        return Created("Registration success", bossRegistrationDto);
    }

    public async Task<ActionResult<string>> Login([FromBody]BossLoginDto bossLoginDto)
    {
        // add validation
        
        var token = await _service.Login(bossLoginDto.Name, bossLoginDto.Password);
        return Ok(token);
    }

    public async Task<ActionResult<BossDto>> GetById([FromBody] Guid id)
    {
        var boss = await _service.GetByIdAsync(id);
        var dto =  _mapper.Map<BossDto>(boss);

        return Ok(dto);
    }

    public async Task<ActionResult<BossDto>> GetByName([FromBody] string name)
    {
        var boss = await _service.GetByNameAsync(name);
        var dto =  _mapper.Map<BossDto>(boss);

        return Ok(dto);
    }
}