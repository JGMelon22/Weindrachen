namespace Weindrachen.Models;

public class GrapeWine
{
    public int GrapeId { get; set; }
    public required Grape Grape { get; set; }
    public int WineId { get; set; }
    public required Wine Wine { get; set; }
}