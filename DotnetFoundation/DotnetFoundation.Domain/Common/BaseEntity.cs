using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Domain.Common;
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public int? ModifiedBy { get; set; }
    public Status Status { get; set; }
}
