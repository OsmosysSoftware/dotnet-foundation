using System.ComponentModel.DataAnnotations;
using DotnetFoundation.Domain.Common;

namespace DotnetFoundation.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = String.Empty;
    public string? LastName { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = String.Empty;
}