using System.ComponentModel.DataAnnotations;

namespace UniqueDraw.Domain.Entities.Base;

public class EntityBase : DomainEntity, IEntityBase<Guid>
{
    [Key]
    public Guid Id { get; set; } = Guid.Empty;

}
