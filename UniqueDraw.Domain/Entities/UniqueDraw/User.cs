using UniqueDraw.Domain.Entities.Base;

namespace UniqueDraw.Domain.Entities.UniqueDraw;
public class User : EntityBase
{
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public ICollection<AssignedNumber> AssignedNumbers { get; set; }  = [];
}
 