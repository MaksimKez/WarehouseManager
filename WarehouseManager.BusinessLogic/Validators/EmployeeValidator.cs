using FluentValidation;
using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.Validators;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(employee => employee.Id).NotEmpty().WithMessage("Id cannot be empty.");
        
        RuleFor(employee => employee.Name).NotEmpty().WithMessage("Name cannot be empty.");
        
        RuleFor(employee => employee.Surname).NotEmpty().WithMessage("Surname cannot be empty.");
        
        RuleFor(employee => employee.Email).NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email address.");
        
        RuleFor(employee => employee.Password).NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        
        RuleFor(employee => employee.Position).IsInEnum().WithMessage("Invalid position.");
        
        RuleFor(employee => employee.CreatedAt).NotEqual(default(DateTime)).WithMessage("CreatedAt must be a valid date.");
    }
}