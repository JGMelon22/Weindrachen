using Riok.Mapperly.Abstractions;
using Weindrachen.DTOs.Grape;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Mappling;

[Mapper]
public static partial class GrapeMapper
{
    public static partial GrapeResult GrapeToGrapeResult(Grape grape);
    public static partial Grape GrapeToGrapeInput(GrapeInput grape);
    public static partial void ApplyUpdate(GrapeInput updateGrape, Grape grape);
}