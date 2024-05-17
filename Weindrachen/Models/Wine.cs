using Weindrachen.Models.Enums;

namespace Weindrachen.Models;

public class Wine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty!;
    public decimal Price { get; set; }
    public bool Doc { get; set; }
    public float AlcoholicLevel { get; set; }
    public Country Country { get; set; }
    public required IList<GrapeWine> GrapeWines { get; set; }
    public required IList<Taste> PossibleTastes { get; set; }
}