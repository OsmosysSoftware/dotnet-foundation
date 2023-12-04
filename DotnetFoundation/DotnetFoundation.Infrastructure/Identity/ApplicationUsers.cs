namespace DotnetFoundation.Infrastructure.Identity;
using DotnetFoundation.Domain.Entities;

public class ApplicationUser : User
{
  public int IdentityApplicationUserId { get; set; }
  public IdentityApplicationUser? IdentityApplicationUser { get; set; }

}