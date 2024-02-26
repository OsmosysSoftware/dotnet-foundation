using DotnetFoundation.Domain.Common;

namespace DotnetFoundation.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
}