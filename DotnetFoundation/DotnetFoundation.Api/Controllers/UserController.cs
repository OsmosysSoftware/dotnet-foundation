using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    [Authorize(Roles = "LEAD")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        List<UserResponse> result = await _userService.GetAllUsersAsync().ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByIdAsync(int userId)
    {
        UserResponse? result = await _userService.GetUserByIdAsync(userId).ConfigureAwait(false);
        return Ok(result);
    }
    [Authorize(Roles = "ADMIN")]
    [HttpPost("add-role")]
    public async Task<IActionResult> AddUserRoleAsync(UserRoleRequest roleRequest)
    {
        bool result = await _userService.AddUserRoleAsync(roleRequest.Email, roleRequest.Role).ConfigureAwait(false);
        return Ok(result);
    }
}