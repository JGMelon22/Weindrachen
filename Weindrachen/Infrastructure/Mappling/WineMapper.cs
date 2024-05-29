using Riok.Mapperly.Abstractions;
using Weindrachen.DTOs.Wine;
using Weindrachen.Models;

namespace Weindrachen.Infrastructure.Mappling;

[Mapper]
public static partial class WineMapper
{
    public static partial WineResult WineToWineResult(Wine wine);
    public static partial Wine WineToWineInput(WineInput wine);
    public static partial void ApplyUpdate(WineInput updateWine, Wine wine);
}