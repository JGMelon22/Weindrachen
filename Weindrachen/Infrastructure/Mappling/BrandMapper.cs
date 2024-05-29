using Riok.Mapperly.Abstractions;
using Weindrachen.DTOs.Brand;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Mappling;

[Mapper]
public static partial class BrandMapper
{
    public static partial BrandResult BrandToBrandResult(Brand brand);
    public static partial Brand BrandToBrandInput(BrandInput brand);
    public static partial void ApplyUpdate(BrandInput updatedBrand, Brand brand);
}