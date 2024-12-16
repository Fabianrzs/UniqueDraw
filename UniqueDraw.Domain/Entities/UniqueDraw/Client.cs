using UniqueDraw.Domain.Entities.Base;

namespace UniqueDraw.Domain.Entities.UniqueDraw;

public class Client : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public ICollection<Raffle> Raffles { get; set; } = [];
    public ICollection<User> Users { get; set; } = [];
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<AssignedNumber> AssignedNumbers { get; set; } = [];
}
