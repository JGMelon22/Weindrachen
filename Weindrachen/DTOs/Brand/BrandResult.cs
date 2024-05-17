using Weindrachen.DTOs.Wine;
using Weindrachen.Models.Enums;

namespace Weindrachen.DTOs.Brand;

public record BrandResult
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty!;
    public Country OriginCountry { get; init; }
}
