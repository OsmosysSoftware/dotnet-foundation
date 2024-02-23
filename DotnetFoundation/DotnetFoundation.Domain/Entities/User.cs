using DotnetFoundation.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
}