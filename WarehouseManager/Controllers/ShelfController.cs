using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.BusinessLogic.Validators;

namespace WarehouseManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShelfController : ControllerBase
{
    private readonly IShelfService _service;
    private readonly ShelfValidator _shelfValidator;

    public ShelfController(IShelfService service, ShelfValidator shelfValidator)
    {
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
        _shelfValidator = shelfValidator ?? throw new ArgumentException("Validator error", nameof(shelfValidator));
    }

    [HttpGet("getById/{id}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Shelf), StatusCodes.Status200OK)]
    public async Task<ActionResult<Shelf>> GetById(Guid id)
    {
        var shelf = await _service.GetByIdAsync(id);
        return Ok(shelf);
    }

    [HttpGet("getAll")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Shelf>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Shelf>>> GetAll()
    {
        var shelves = await _service.GetAllAsync();
        return Ok(shelves.ToList());
    }

    [HttpPost("add")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Add([FromBody] Shelf shelf)
    {
        var validationResult = await _shelfValidator.ValidateAsync(shelf);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var guid = await _service.AddAsync(shelf);
        return Created(nameof(GetById), guid);
    }

    [HttpPut("update")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update([FromBody] Shelf shelf)
    {
        var validationResult = await _shelfValidator.ValidateAsync(shelf);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _service.UpdateAsync(shelf);
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
