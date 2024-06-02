namespace Weindrachen.Models;

public class GrapeWine
{
    public int GrapeId { get; set; }
    public Grape Grape { get; set; } = null!;
    public int WineId { get; set; }
    public Wine Wine { get; set; } = null!;
}