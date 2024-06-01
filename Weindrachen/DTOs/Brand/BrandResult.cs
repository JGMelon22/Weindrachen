using System.Text.Json.Serialization;
using Weindrachen.Models.Enums;

namespace Weindrachen.DTOs.Brand;

public record BrandResult
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Country Country { get; init; }
}