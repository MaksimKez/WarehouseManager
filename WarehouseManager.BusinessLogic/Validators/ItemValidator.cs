using FluentValidation;
using Microsoft.AspNetCore.SignalR.Protocol;
using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.Validators;

public class ItemValidator : AbstractValidator<Item>
{
    public ItemValidator()
    {
        RuleFor(item => item.Id).NotEmpty().WithMessage("Id cannot be empty.");

        RuleFor(item => item.Description).NotEmpty().WithMessage("Description cannot be empty.")
            .MinimumLength(10).WithMessage("Description cannot be short.");
    }
}