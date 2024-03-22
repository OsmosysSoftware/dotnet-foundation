using DotnetFoundation.Api.Helpers;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Interfaces.Validator;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.UserDTO;
using DotnetFoundation.Application.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    private readonly IUserValidator _userValidator;

    public UserController(IUserService userService, IUserValidator userValidator)
    {
        _userService = userService;
        _userValidator = userValidator;
    }

    /// <summary>
    /// Get all user records.
    /// Authorize - LEAD role
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "LEAD")]
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
            response.Errors = GetErrorResponse();
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="userId">Id of user record</param>
    [HttpGet("{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<UserResponse>>> GetUserByIdAsync(int userId)
    {
        BaseResponse<UserResponse> response = new(ResponseStatus.Fail);
        try
        {
            bool IsValidUserId = await _userValidator.ValidUserId(userId).ConfigureAwait(false);
            if (!IsValidUserId)
            {
                ModelState.AddModelError("userId", "User Not Found");
                throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
            }
            response.Data = await _userService.GetUserByIdAsync(userId).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (UserNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Errors = GetErrorResponse();
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Add new user role.
    /// Authorize - ADMIN role
    /// </summary>
    /// <param name="request">Role request details</param>
    [HttpPost("addrole")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<bool>>> AddUserRoleAsync(UserRoleRequest request)
    {
        BaseResponse<bool> response = new(ResponseStatus.Fail);
        try
        {
            bool isValidEmail = await _userValidator.ValidEmailId(request.Email).ConfigureAwait(false);
            if (!isValidEmail)
            {
                ModelState.AddModelError("email", "Error Finding User");
                throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
            }
            response.Data = await _userService.AddUserRoleAsync(request).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (UserNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Errors = GetErrorResponse();
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  Update user details by user id.
    /// </summary>
    /// <param name="userId">Id of user record</param>
    /// <param name="request">user details updation request</param>
    [HttpPut("{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<UserResponse>>> UpdateUserAsync(int userId, UpdateUserRequest request)
    {
        BaseResponse<UserResponse> response = new(ResponseStatus.Fail);
        try
        {
            bool IsValidUserId = await _userValidator.ValidUserId(userId).ConfigureAwait(false);
            if (!IsValidUserId)
            {
                ModelState.AddModelError("userId", "User Not Found");
                throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
            }
            response.Data = await _userService.UpdateUserAsync(userId, request).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (UserNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Errors = GetErrorResponse();
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
            bool IsValidUserId = await _userValidator.ValidUserId(userId).ConfigureAwait(false);
            if (!IsValidUserId)
            {
                ModelState.AddModelError("userId", "User Not Found");
                throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
            }
            response.Data = await _userService.DeleteUserAsync(userId).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (UserNotFoundException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
    /// <summary>
    /// Change user Password.
    /// </summary>
    /// <param name="request">Change password request</param>
    [HttpPut("changepassword")]
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
        catch (IdentityUserException ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return BadRequest(response);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = ResponseStatus.Error;
            response.Errors = GetErrorResponse();
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
