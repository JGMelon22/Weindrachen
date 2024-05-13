using Weindrachen.Models.Enums;

namespace Weindrachen.Models;

public class Brand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty!;
    public Country OriginCountry { get; set; }
}