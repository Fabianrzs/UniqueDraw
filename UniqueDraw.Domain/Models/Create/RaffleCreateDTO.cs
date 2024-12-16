namespace UniqueDraw.Domain.Models.Create;

public class RaffleCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid ClientId { get; set; }
}