using FluentValidation;
using Weindrachen.DTOs.Brand;
using Weindrachen.DTOs.Grape;
using Weindrachen.DTOs.Wine;
using Weindrachen.Infrastructure.Validators;

namespace Weindrachen.Configuration;

public static class ValidatorsCollection
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<BrandInput>, BrandValidator>();
        services.AddScoped<IValidator<GrapeInput>, GrapeValidator>();
        services.AddScoped<IValidator<WineInput>, WineValidator>();

        return services;
    }
}