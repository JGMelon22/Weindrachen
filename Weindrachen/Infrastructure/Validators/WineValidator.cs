using System.Globalization;
using FluentValidation;
using Weindrachen.DTOs.Wine;
using Weindrachen.Models.Enums;

namespace Weindrachen.Infrastructure.Validators;

public class WineValidator : AbstractValidator<WineInput>
{
    private readonly float _maxAlcoholicLevel = 25.0F;
    private readonly decimal _maxPrice = 99999.99M;
    private readonly float _minAlcoholicLevel = 5.5F;
    private readonly decimal _minPrice = 1.0M;

    public WineValidator()
    {
        RuleFor(w => w.Name)
            .NotEmpty()
            .WithMessage("Wine Name can not be empty!")
            .NotNull()
            .WithMessage("Wine Name can not be null!")
            .MinimumLength(2)
            .WithMessage("Wine Name must contain at least 2 characters!")
            .MaximumLength(100)
            .WithMessage("Wine Name can not exceed 100 characters!");

        RuleFor(w => w.Price)
            .NotEmpty()
            .WithMessage("Wine Price can not be empty!")
            .NotNull()
            .WithMessage("Wine Price can not be null!")
            .Must(price => price is >= 1.0M and <= 99999.99M)
            .WithMessage(
                $"Wine Price must be between {_minPrice.ToString("C", CultureInfo.InvariantCulture)} and {_maxPrice.ToString("C", CultureInfo.InvariantCulture)}!");

        RuleFor(w => w.IsDoc)
            .NotNull()
            .WithMessage("Is Doc status can not be null!");

        RuleFor(w => w.AlcoholicLevel)
            .NotEmpty()
            .WithMessage("Alcoholic Level can not be empty!")
            .NotNull()
            .WithMessage("Alcoholic Level can not be null!")
            .Must(alcoholicLevel => alcoholicLevel is >= 5.5F and <= 25.0F)
            .WithMessage($"Wine Alcoholic Level must be between {_minAlcoholicLevel}% and {_maxAlcoholicLevel}%!");

        RuleFor(w => w.Country)
            .NotEmpty()
            .WithMessage("An origin Country must be informed!")
            .NotNull()
            .WithMessage("An origin Country must be informed!")
            .Must(country => Enum.IsDefined(typeof(Country), country))
            .WithMessage("A valid origin Country must be informed!");

        RuleFor(w => w.BrandId)
            .NotEmpty()
            .WithMessage("A BrandId must be informed!")
            .NotNull()
            .WithMessage("A BrandId must be informed!");

        RuleFor(w => w.GrapeWines)
            .NotEmpty()
            .WithMessage("A valid GrapeId must be informed!")
            .NotNull()
            .WithMessage("A valid GrapeId must be informed!");

        RuleFor(w => w.Taste)
            .NotEmpty()
            .WithMessage("A Predominant Flavour must be informed!")
            .NotNull()
            .WithMessage("An Predominant Flavour must be informed!")
            .Must(taste => Enum.IsDefined(typeof(Taste), taste))
            .WithMessage("A Predominant Flavour must be informed!");
    }
}