using DotnetFoundation.Domain.Enums;

namespace DotnetFoundation.Domain.Common;

/// <summary>
/// Provides a base class for entities with common fields.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier for the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date and time the entity was created.
    /// </summary>
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// </summary>
    public int CreatedBy { get; set; }
    /// <summary>
    /// Gets or sets the date and time the entity was last modified.
    /// </summary>
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Gets or sets the identifier of the user who last modified the entity.
    /// </summary>
    public int ModifiedBy { get; set; }
    /// <summary>
    /// Gets or sets the status of the entity.
    /// </summary>
    public Status Status { get; set; } = Status.ACTIVE;
}
