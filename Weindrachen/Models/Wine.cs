using Weindrachen.Models.Enums;

namespace Weindrachen.Models;

public class Wine
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty!;
    public decimal Price { get; set; }
    public bool Doc { get; set; }
    public float AlcoholicLevel { get; set; }
    public Country Country { get; set; }
    public List<Grape> Grapes { get; set; } = new List<Grape>();
}