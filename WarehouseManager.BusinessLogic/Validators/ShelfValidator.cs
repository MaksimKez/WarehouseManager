using FluentValidation;
using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.Validators;

public class ShelfValidator : AbstractValidator<Shelf>
{
    public ShelfValidator()
    {
        RuleFor(shelf => shelf.Id).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(shelf => shelf.Description).NotEmpty().WithMessage("Description cannot be empty")
            .MinimumLength(10).WithMessage("Description is too short");
    }
}