using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.Controllers;

public class TodoController : ControllerBase
{
    private readonly ITodoService _service;

    public TodoController(ITodoService service)
    {
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    public async Task<ActionResult<Todo>> GetById([FromBody] Guid id)
    {
        var todo = await _service.GetByIdAsync(id);
        return Ok(todo);
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetAll()
    {
        var todos = await _service.GetAllAsync();
        return Ok(todos.ToList());
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByEmployeeId([FromBody] Guid employeeId)
    {
        var todos = await _service.GetByEmployeeIdAsync(employeeId);
        return Ok(todos.ToList());
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByShelfIf([FromBody] Guid id)
    {
        var todos = await _service.GetByShelfIdAsync(id);
        return Ok(todos.ToList());
    }
    
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByItemId([FromBody] Guid id)
    {
        var todos = await _service.GetByItemIdAsync(id);
        return Ok(todos.ToList());
    }
    
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByIsDoneStatus([FromBody] bool isDone)
    {
        var todos = await _service.GetByIsDoneStatusAsync(isDone);
        return Ok(todos.ToList());
    }
    
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByCreatedAtRange([FromBody] DateTime startDate, [FromBody] DateTime endDate)
    {
        var todos = await _service.GetByCreatedAtRangeAsync(startDate, endDate);
        return Ok(todos.ToList());
    }

    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> Add([FromBody] Todo todo)
    {
        var guid = await _service.AddAsync(todo);
        return Created(nameof(Guid), guid);
    }
    
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update([FromBody] Todo todo)
    {
        await _service.UpdateAsync(todo);
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