namespace Weindrachen.Models;

public class ServiceResponse<T> 
{
    public T? Data { get; set; }
    public bool MyProperty { get; set; } = true;
    public string? Message { get; set; }
}