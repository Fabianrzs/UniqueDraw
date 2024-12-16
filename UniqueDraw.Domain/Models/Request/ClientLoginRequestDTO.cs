namespace UniqueDraw.Domain.Models.Request;

public class ClientLoginRequestDTO
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
