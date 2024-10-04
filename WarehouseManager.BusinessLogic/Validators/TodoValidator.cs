using FluentValidation;
using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.Validators;

public class TodoValidator : AbstractValidator<Todo>
{
    public TodoValidator()
    {
        RuleFor(todo => todo.Id).NotEmpty().WithMessage("Id is required."); 
        
        RuleFor(todo => todo.Title).NotEmpty().WithMessage("Title is required.")
            .MaximumLength(20).WithMessage("Title must be less than 100 characters.");
        
        RuleFor(todo => todo.Description).MaximumLength(200).WithMessage("Description must be less than 500 characters.");
        
        RuleFor(todo => todo.IsDone).NotNull().WithMessage("IsDone status is required.");
        
        RuleFor(todo => todo.CreatedAt).NotEmpty().WithMessage("CreatedAt is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedAt cannot be in the future.");
        
        RuleFor(todo => todo.Deadline).GreaterThan(todo => todo.CreatedAt).WithMessage("Deadline must be after CreatedAt.");
        
        RuleFor(todo => todo.EmployeeId).NotEmpty().WithMessage("EmployeeId is required.");
        
        RuleFor(todo => todo.ShelfId).NotEmpty().WithMessage("ShelfId is required.");
        
        RuleFor(todo => todo.ItemId).NotEmpty().WithMessage("ItemId is required.");
    }
}