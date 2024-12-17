namespace UniqueDraw.Domain.Models.Response;

public class ClientResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? Token { get; set; }
}
