using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.DTOs.Wine;

public record WineResult
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty!;
    public decimal Price { get; init; }
    public bool IsDoc { get; init; }
    public float AlcoholicLevel { get; init; }
    public Country Country { get; init; }
    public List<GrapeWine> Grapes { get; init; } = new();
    public List<Taste> PossibleTastes { get; init; } = new();
}