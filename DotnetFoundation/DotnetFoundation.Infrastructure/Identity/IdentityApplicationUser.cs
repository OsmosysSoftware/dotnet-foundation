using DotnetFoundation.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DotnetFoundation.Infrastructure.Identity;

public class IdentityApplicationUser : IdentityUser<int>
{
  public ApplicationUser ApplicationUser { get; set; } = null!;
}