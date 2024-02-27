using System.ComponentModel.DataAnnotations;
using DotnetFoundation.Domain.Common;

namespace DotnetFoundation.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Country { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}