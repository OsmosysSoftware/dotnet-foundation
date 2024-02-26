using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetFoundation.Application.Models.DTOs.UserDTO;

public record UpdateUserRequest(string FirstName, string? LastName, string? Country, string? PhoneNumber);