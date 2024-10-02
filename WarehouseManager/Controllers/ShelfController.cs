using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.Controllers;

public class ShelfController : ControllerBase
{
    private readonly IShelfService _service;

    public ShelfController(IShelfService service)
    {
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Shelf), StatusCodes.Status200OK)]
    public async Task<ActionResult<Shelf>> GetById([FromBody] Guid id)
    {
        var shelf = await _service.GetByIdAsync(id);
        return Ok(shelf);
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Shelf>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Shelf>>> GetAll()
    {
        var shelves = await _service.GetAllAsync();
        return Ok(shelves.ToList());
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> Add(Shelf shelf)
    {
        var guid = await _service.AddAsync(shelf);
        return Created(nameof(GetById), guid);
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update(Shelf shelf)
    {
        await _service.UpdateAsync(shelf);
        return Ok();
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete([FromBody] Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}