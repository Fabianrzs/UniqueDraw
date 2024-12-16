namespace UniqueDraw.Domain.Models.Request;

public class AssignedNumberRequestDTO
{
    public Guid UserId { get; set; }
    public Guid ClientId { get; set; }
    public Guid RaffleId { get; set; }
}
