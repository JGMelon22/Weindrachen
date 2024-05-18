namespace Weindrachen.Models;

public class Grape
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty!;
    public List<GrapeWine> GrapeWines { get; set; } = new();
}