namespace UniqueDraw.Domain.Models.Create;

public class ProductCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
}
