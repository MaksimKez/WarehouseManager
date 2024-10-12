using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.BusinessLogic.Validators;

namespace WarehouseManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _service;
    private readonly ItemValidator _itemValidator;

    public ItemController(IItemService service, ItemValidator itemValidator)
    {
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
        _itemValidator = itemValidator ?? throw new ArgumentException("Validator error", nameof(itemValidator));
    }

    [HttpGet("getById/{id}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    public async Task<ActionResult<Item>> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return Ok(item);
    }
    
    [HttpGet("getByFragileStatus/{isFragile}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    public async Task<ActionResult<Item>> GetByFragileStatus(bool isFragile)
    {
        var item = await _service.GetByFragileStatusAsync(isFragile);
        return Ok(item);
    }

    [HttpGet("getAll")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Item>>> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items.ToList());
    }

    [HttpPost("add")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Add([FromBody] Item item)
    {
        var validationResult = await _itemValidator.ValidateAsync(item);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var guid = await _service.AddAsync(item);
        return Created(nameof(GetById), guid);
    }

    [HttpPut("update")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update([FromBody] Item item)
    {
        var validationResult = await _itemValidator.ValidateAsync(item);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _service.UpdateAsync(item);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
