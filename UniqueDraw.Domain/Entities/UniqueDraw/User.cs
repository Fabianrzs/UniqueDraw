using UniqueDraw.Domain.Attributes;
using UniqueDraw.Domain.Entities.Base;

namespace UniqueDraw.Domain.Entities.UniqueDraw;
public class User : EntityBase
{
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = new();
    [Encrypt]
    public string Name { get; set; } = string.Empty;
    [Encrypt]
    public string? Email { get; set; }
    [Encrypt]
    public string? Phone { get; set; }
    public ICollection<AssignedNumber> AssignedNumbers { get; set; }  = [];
}
 