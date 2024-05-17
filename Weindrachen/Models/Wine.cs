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
    public Brand Brand { get; set; } = null!;
    public IList<GrapeWine> GrapeWines { get; set; } = null!;
    public IList<Taste> PossibleTastes { get; set; } = null!;
}