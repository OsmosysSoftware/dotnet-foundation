using DotnetFoundation.Domain.Common;

namespace DotnetFoundation.Domain.Entities;

public class User : BaseEntity
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
}