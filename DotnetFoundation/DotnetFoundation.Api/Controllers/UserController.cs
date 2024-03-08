using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Application.Models.Enums;
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
    /// </summary>
    [HttpGet]
    [Authorize]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<PagedList<UserResponse>>>> GetAllUsersAsync([FromQuery] PagingRequest pagingRequest)
    {
        BaseResponse<PagedList<UserResponse>> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _userService.GetAllUsersAsync(pagingRequest).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Get user by id.
    /// Authorize - LEAD role
    /// </summary>
    /// <param name="userId">Id of user record</param>
    [HttpGet("{userId}")]
    [Authorize(Roles = "LEAD")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<UserResponse>>> GetUserByIdAsync(int userId)
    {
        BaseResponse<UserResponse> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _userService.GetUserByIdAsync(userId).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Add new user role.
    /// Authorize - ADMIN role
    /// </summary>
    /// <param name="roleRequest">Role request details</param>
    [HttpPost("addrole")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<bool>>> AddUserRoleAsync(UserRoleRequest roleRequest)
    {
        BaseResponse<bool> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _userService.AddUserRoleAsync(roleRequest.Email, roleRequest.Role).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  Update user details by user id.
    /// </summary>
    /// <param name="userId">Id of user record</param>
    /// <param name="updateUserRequest">user details updation request</param>
    [HttpPut("{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<UserResponse>>> UpdateUserAsync(int userId, UpdateUserRequest updateUserRequest)
    {
        BaseResponse<UserResponse> response = new(ResponseStatus.Fail);
        if (updateUserRequest == null)
        {
            return BadRequest("Invalid request data");
        }
        try
        {
            response.Data = await _userService.UpdateUserAsync(userId, updateUserRequest).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return BadRequest(response);
        }
        catch (UserException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Delete user by id.
    /// </summary>
    /// <param name="userId">Id of user record</param>
    [HttpDelete("{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<UserResponse>>> DeleteUserAsync(int userId)
    {
        BaseResponse<UserResponse> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _userService.DeleteUserAsync(userId).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
    /// <summary>
    /// Change user Password.
    /// </summary>
    /// <param name="request">Change password request</param>
    [HttpPut("changepassword")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<int>>> ChangePasswordAsync(UserChangePassword request)
    {
        BaseResponse<int> response = new(ResponseStatus.Fail);
        try
        {
            await _userService.ChangePasswordAsync(request).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return BadRequest(response);
        }
        catch (IdentityUserException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}

