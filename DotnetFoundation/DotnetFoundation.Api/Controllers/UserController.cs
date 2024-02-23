using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Domain.Entities;
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
    public async Task<ActionResult<BaseResponse<List<UserResponse>>>> GetAllUsersAsync()
    {
        BaseResponse<List<UserResponse>> response = new(ResponseStatus.Fail);

        response.Data = await _userService.GetAllUsersAsync().ConfigureAwait(false);
        response.Status = ResponseStatus.Success;
        
        return Ok(response);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<BaseResponse<UserResponse>>> GetUserByIdAsync(int userId)
    {
        BaseResponse<UserResponse> response = new(ResponseStatus.Fail);

        response.Data = await _userService.GetUserByIdAsync(userId).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost("add-role")]
    public async Task<ActionResult<BaseResponse<bool>>> AddUserRoleAsync(UserRoleRequest roleRequest)
    {
        BaseResponse<bool> response = new(ResponseStatus.Fail);

        response.Data = await _userService.AddUserRoleAsync(roleRequest.Email, roleRequest.Role).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }
}
