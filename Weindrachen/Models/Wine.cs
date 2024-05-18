using Weindrachen.Models.Enums;

namespace Weindrachen.Models;

public class Wine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty!;
    public decimal Price { get; set; }
    public bool IsDoc { get; set; }
    public float AlcoholicLevel { get; set; }
    public Country OriginCountry { get; set; }
    public int BrandId { get; set; }
    public required Brand Brand { get; set; }
    public List<GrapeWine> GrapeWines { get; set; } = new();
    public List<Taste> PossibleTastes { get; set; } = new();
}