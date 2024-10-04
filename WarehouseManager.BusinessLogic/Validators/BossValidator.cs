using FluentValidation;
using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.Validators;

public class BossValidator : AbstractValidator<Boss>
{
    public BossValidator()
    {
        RuleFor(boss => boss.Id).NotEmpty().WithMessage("Id cannot be empty.");
        
        RuleFor(boss => boss.Name).NotEmpty().WithMessage("Name cannot be empty.");
        
        RuleFor(boss => boss.Surname).NotEmpty().WithMessage("Surname cannot be empty.");
        
        RuleFor(boss => boss.Email).NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email address.");
        
        RuleFor(boss => boss.Password).NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        
        RuleFor(boss => boss.CreatedAt).NotEqual(default(DateTime)).WithMessage("CreatedAt must be a valid date.");
    }
}
