using System.Text.Json.Serialization;
using Weindrachen.DTOs.GrapeWine;
using Weindrachen.Models.Enums;

namespace Weindrachen.DTOs.Wine;

public record WineInput(
    string Name,
    decimal Price,
    bool IsDoc,
    float AlcoholicLevel,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    Country Country,
    int BrandId,
    ICollection<GrapeWineInput> GrapeWines,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    Taste Taste
);