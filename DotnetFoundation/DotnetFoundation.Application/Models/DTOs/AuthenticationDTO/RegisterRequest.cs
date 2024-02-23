namespace DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
public record RegisterRequest(string FirstName, string LastName, string Email, string Password);