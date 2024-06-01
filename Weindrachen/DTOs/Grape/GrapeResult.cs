using System.Text.Json.Serialization;
using Weindrachen.Models.Enums;

namespace Weindrachen.DTOs.Grape;

public record GrapeResult
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty!;
}