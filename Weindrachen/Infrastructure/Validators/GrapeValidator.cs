using FluentValidation;
using Weindrachen.DTOs.Grape;

namespace Weindrachen.Infrastructure.Validators;

public class GrapeValidator : AbstractValidator<GrapeInput>
{
    public GrapeValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty()
            .WithMessage("Grape Name can not be empty!")
            .NotNull()
            .WithMessage("Grape Name can not be null!")
            .MinimumLength(2)
            .WithMessage("Grape Name must contain at least 2 characters!")
            .MaximumLength(100)
            .WithMessage("Brand Name can not exceed 100 characters!");
    }
}