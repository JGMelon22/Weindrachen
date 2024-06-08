using System.Text.Json.Serialization;
using Weindrachen.Models.Enums;

namespace Weindrachen.DTOs.BrandGrapeWine;

public record BrandGrapeWineResult
{
    // Wine Data
    public int WineId { get; init; }
    public string WineName { get; set; } = string.Empty!;
    public decimal Price { get; set; }
    public bool IsDoc { get; set; }
    public float AlcoholicLevel { get; init; }

    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    public Country Country { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Taste Taste { get; init; }

    // Brand Data
    public string BrandName { get; set; } = string.Empty!;

    // Grape Data
    public string GrapeName { get; set; } = string.Empty!;
}