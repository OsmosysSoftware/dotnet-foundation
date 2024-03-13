using DotnetFoundation.Api.Helpers;
using DotnetFoundation.Application.Exceptions;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Interfaces.Validator;
using DotnetFoundation.Application.Models.Common;
using DotnetFoundation.Application.Models.DTOs.AuthenticationDTO;
using DotnetFoundation.Application.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFoundation.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserValidator _userValidator;
    public AuthenticationController(IAuthenticationService authenticationService, IUserValidator userValidator)
    {
        _authenticationService = authenticationService;
        _userValidator = userValidator;
    }

    /// <summary>
    /// User registration.
    /// </summary>
    /// <param name="request">New user registration request</param>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<AuthenticationResponse>>> RegisterAsync(RegisterRequest request)
    {
        BaseResponse<AuthenticationResponse> response = new(ResponseStatus.Fail);
        try
        {
            bool isRegisteredEmail = await _userValidator.IsEmailRegistered(request.Email).ConfigureAwait(false);
            if (isRegisteredEmail)
            {
                ModelState.AddModelError("email", "Email already in use");
                throw new IdentityUserException(ErrorValues.GenricValidationMessage);
            }
            response.Data = await _authenticationService.RegisterAsync(request).ConfigureAwait(false);
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
            response.Errors = GetErrorResponse();
            response.Status = ResponseStatus.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// User login.
    /// </summary>
    /// <param name="request">User login request</param>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<AuthenticationResponse>>> LoginAsync(LoginRequest request)
    {
        BaseResponse<AuthenticationResponse> response = new(ResponseStatus.Fail);
        try
        {
            response.Data = await _authenticationService.LoginAsync(request).ConfigureAwait(false);
            response.Status = ResponseStatus.Success;

            return Ok(response);
        }
        catch (InvalidCredentialsException ex)
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
    /// User password reset using reset token.
    /// </summary>
    /// <param name="request">New password details request</param>
    [HttpPost("resetpassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<int>>> ResetPasswordAsync(PasswordResetRequest request)
    {
        BaseResponse<int> response = new(ResponseStatus.Fail);
        try
        {
            bool isValidEmail = await _userValidator.ValidEmailId(request.Email).ConfigureAwait(false);
            if (!isValidEmail)
            {
                ModelState.AddModelError("email", "Error Finding User");
                throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
            }
            await _authenticationService.ResetPasswordAsync(request).ConfigureAwait(false);
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
        catch (InvalidTokenException ex)
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
    /// Forgot user password.
    /// </summary>
    /// <param name="email">Email of user to reset password</param>
    [HttpPost("forgotpassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BaseResponse<int>>> ForgotPasswordAsync(string email)
    {
        BaseResponse<int> response = new(ResponseStatus.Fail);
        try
        {
            bool isValidEmail = await _userValidator.ValidEmailId(email).ConfigureAwait(false);
            if (!isValidEmail)
            {
                ModelState.AddModelError("email", "Error Finding User");
                throw new UserNotFoundException(ErrorValues.GenricNotFoundMessage);
            }
            await _authenticationService.ForgotPasswordAsync(email).ConfigureAwait(false);
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
}
