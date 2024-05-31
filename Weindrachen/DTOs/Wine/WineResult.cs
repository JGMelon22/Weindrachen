using System.Text.Json.Serialization;
using Weindrachen.DTOs.Grape;
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
    public int GrapeId { get; init; }
    public IEnumerable<GrapeResult> Grapes { get; init; } = new List<GrapeResult>();

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Taste Taste { get; init; }
}