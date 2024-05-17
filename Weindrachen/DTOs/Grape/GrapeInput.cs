using Weindrachen.Models;

namespace Weindrachen.DTOs.Grape;

public record GrapeInput(string Name, ICollection<GrapeWine> Wines);
