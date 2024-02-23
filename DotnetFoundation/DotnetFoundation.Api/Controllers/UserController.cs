using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
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

    /// <summary>
    /// Get all user records.
    /// Authorize - LEAD role
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "LEAD")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<List<UserResponse>>>> GetAllUsersAsync()
    {
        BaseResponse<List<UserResponse>> response = new(ResponseStatus.Fail);

        response.Data = await _userService.GetAllUsersAsync().ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="userId">Id of user record</param>
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<UserResponse>>> GetUserByIdAsync(int userId)
    {
        BaseResponse<UserResponse> response = new(ResponseStatus.Fail);

        response.Data = await _userService.GetUserByIdAsync(userId).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }

    /// <summary>
    /// Add new user role.
    /// Authorize - ADMIN role
    /// </summary>
    /// <param name="roleRequest">Role request details</param>
    [HttpPost("add-role")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<bool>>> AddUserRoleAsync(UserRoleRequest roleRequest)
    {
        BaseResponse<bool> response = new(ResponseStatus.Fail);

        response.Data = await _userService.AddUserRoleAsync(roleRequest.Email, roleRequest.Role).ConfigureAwait(false);
        response.Status = ResponseStatus.Success;

        return Ok(response);
    }
}
