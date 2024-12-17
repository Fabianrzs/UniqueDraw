namespace UniqueDraw.Domain.Entities.Base;

public class DomainEntity
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
