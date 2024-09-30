using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.Controllers;

public class ItemController : ControllerBase
{
    private readonly IItemService _service;

    public ItemController(IItemService service, IMapper mapper)
    {
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    public async Task<ActionResult<Item>> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        
        return Ok(item);
    }
    
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
    public async Task<ActionResult<Item>> GetByFragileStatus(bool isFragile)
    {
        var item = await _service.GetByFragileStatusAsync(isFragile);
        
        return Ok(item);
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Item>>> GetAll()
    {
        var items = await _service.GetAllAsync();
        
        return Ok(items.ToList());
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Add(Item item)
    {
        await _service.AddAsync(item);
        return Ok();
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update(Item item)
    {
        await _service.UpdateAsync(item);
        return Ok();
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}