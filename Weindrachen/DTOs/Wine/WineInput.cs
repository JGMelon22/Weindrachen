using Weindrachen.DTOs.GrapeWine;
using Weindrachen.Models.Enums;

namespace Weindrachen.DTOs.Wine;

public record WineInput(
    string Name,
    decimal Price,
    bool IsDoc,
    float AlcoholicLevel,
    Country Country,
    int BrandId,
    ICollection<GrapeWineInput> Grapes,
    Taste PredominantFlavour
);