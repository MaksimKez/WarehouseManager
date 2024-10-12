using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BusinessLogic.ContractsServices;
using WarehouseManager.BusinessLogic.Models;
using WarehouseManager.BusinessLogic.Validators;

namespace WarehouseManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;
    private readonly TodoValidator _todoValidator;

    public TodoController(ITodoService service, TodoValidator todoValidator)
    {
        _service = service ?? throw new ArgumentException("Service error", nameof(service));
        _todoValidator = todoValidator ?? throw new ArgumentException("Validator error", nameof(todoValidator));
    }

    [HttpGet("getById/{id}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    public async Task<ActionResult<Todo>> GetById(Guid id)
    {
        var todo = await _service.GetByIdAsync(id);
        return Ok(todo);
    }

    [HttpGet("getAll")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetAll()
    {
        var todos = await _service.GetAllAsync();
        return Ok(todos.ToList());
    }

    [HttpGet("getByEmployeeId/{employeeId}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByEmployeeId(Guid employeeId)
    {
        var todos = await _service.GetByEmployeeIdAsync(employeeId);
        return Ok(todos.ToList());
    }

    [HttpGet("getByShelfId/{id}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByShelfId(Guid id)
    {
        var todos = await _service.GetByShelfIdAsync(id);
        return Ok(todos.ToList());
    }

    [HttpGet("getByItemId/{id}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByItemId(Guid id)
    {
        var todos = await _service.GetByItemIdAsync(id);
        return Ok(todos.ToList());
    }

    [HttpGet("getByIsDoneStatus/{isDone}")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByIsDoneStatus(bool isDone)
    {
        var todos = await _service.GetByIsDoneStatusAsync(isDone);
        return Ok(todos.ToList());
    }

    [HttpGet("getByCreatedAtRange")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(List<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Todo>>> GetByCreatedAtRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var todos = await _service.GetByCreatedAtRangeAsync(startDate, endDate);
        return Ok(todos.ToList());
    }

    [HttpPost("add")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Add([FromBody] Todo todo)
    {
        var validationResult = await _todoValidator.ValidateAsync(todo);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var guid = await _service.AddAsync(todo);
        return Created(nameof(GetById), guid);
    }

    [HttpPut("update")]
    [Authorize(Policy = "EmployeePolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update([FromBody] Todo todo)
    {
        var validationResult = await _todoValidator.ValidateAsync(todo);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _service.UpdateAsync(todo);
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
