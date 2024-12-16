namespace UniqueDraw.Domain.Models.Create;

public class UserCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
}
