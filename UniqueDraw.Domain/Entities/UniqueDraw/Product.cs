using UniqueDraw.Domain.Entities.Base;

namespace UniqueDraw.Domain.Entities.UniqueDraw;
public class Product : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
