namespace UniqueDraw.Domain.Models.Response;

public class RaffleResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid ClientId { get; set; }
}
