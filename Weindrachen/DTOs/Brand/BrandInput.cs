using System.Text.Json.Serialization;
using Weindrachen.Models.Enums;

namespace Weindrachen.DTOs.Brand;

public record BrandInput(
    string Name,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    Country Country
);