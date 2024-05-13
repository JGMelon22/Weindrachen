using Weindrachen.Models.Enums;

namespace Weindrachen.Models;

public class Grape
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty!;
    public List<Country> Countries { get; set; } = new List<Country>();
}