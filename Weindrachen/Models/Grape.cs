namespace Weindrachen.Models;

public class Grape
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty!;
    public required IList<GrapeWine> GrapeWines { get; set; }
}