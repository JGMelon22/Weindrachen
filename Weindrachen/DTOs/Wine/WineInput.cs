using Weindrachen.Models;
using Weindrachen.Models.Enums;

namespace Weindrachen.DTOs.Wine;

public record WineInput(
    string Name,
    decimal Price,
    bool IsDoc,
    float AlcoholicLevel,
    Country Country,
    ICollection<GrapeWine> Wines,
    ICollection<Taste> PossibleTastes
);
