using UniqueDraw.Domain.Entities.Base;

namespace UniqueDraw.Domain.Entities.UniqueDraw;
public class Raffle : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public ICollection<AssignedNumber> AssignedNumbers { get; set; } = [];
}
