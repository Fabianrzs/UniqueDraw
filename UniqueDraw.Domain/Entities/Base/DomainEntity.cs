namespace UniqueDraw.Domain.Entities.Base;

public class DomainEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime LastModifiedOn { get; set; }
    public bool IsActive { get; set; } = true;
}
