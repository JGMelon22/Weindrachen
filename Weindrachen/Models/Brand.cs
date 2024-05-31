using Weindrachen.Models.Enums;

namespace Weindrachen.Models;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty!;
    public Country Country { get; set; }
    public IList<Wine> Wines { get; set; } = new List<Wine>();
}