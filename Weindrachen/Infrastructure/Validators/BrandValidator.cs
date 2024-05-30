using FluentValidation;
using Weindrachen.DTOs.Brand;
using Weindrachen.Models.Enums;

namespace Weindrachen.Infrastructure.Validators;

public class BrandValidator : AbstractValidator<BrandInput>
{
    public BrandValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .WithMessage("Brand Name can not be empty!")
            .NotNull()
            .WithMessage("Brand Name can not bee null!")
            .MinimumLength(2)
            .WithMessage("Brand Name must contain at least 2 characters!")
            .MaximumLength(100)
            .WithMessage("Brand Name can not exceed 100 characters!");

        RuleFor(b => b.Country)
            .NotEmpty()
            .WithMessage("An origin Country must be informed!")
            .NotNull()
            .WithMessage("An origin Country must be informed!")
            .Must(country => Enum.IsDefined(typeof(Country), country))
            .WithMessage("A valid origin Country must be informed!");
    }
}