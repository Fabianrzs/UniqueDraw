namespace UniqueDraw.Domain.Models.Response;

public class AssignedNumberResponseDTO
{
    public string Number { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid ClientId { get; set; }
    public Guid RaffleId { get; set; }
}
