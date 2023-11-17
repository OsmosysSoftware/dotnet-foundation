using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DotnetBoilerPlate.API.BLL.Interfaces;
using DotnetBoilerPlate.API.Helpers;
using DotnetBoilerPlate.API.Models;
using DotnetBoilerPlate.DAL.DatabaseContext;
using DotnetBoilerPlate.DAL.Models;
using DotnetBoilerPlate.DAL.Repositories.Interfaces;

namespace DotnetBoilerPlate.API.BLL
{
    public class UsersBLL : Controller, IUsersBLL
    {
        private readonly IUsersRepo _usersRepo;
        private readonly ILogger<UsersBLL> _logger;
        private readonly IConfiguration _configuration;
        private readonly SqlDatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UsersBLL(IUsersRepo userRepo, ILogger<UsersBLL> logger, IConfiguration configuration, SqlDatabaseContext databaseContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment)
        {
            _usersRepo = userRepo;
            _logger = logger;
            _configuration = configuration;
            _databaseContext = databaseContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult<BaseResponse<List<UsersDBO>>> GetAllUsers()
        {
            BaseResponse<List<UsersDBO>> response = new BaseResponse<List<UsersDBO>>(ResponseStatus.Fail);

            try
            {
                // Get all users
                response.Data = _usersRepo.GetAllUsers();
                response.Status = ResponseStatus.Success;
            }
            catch (BadHttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                response.Status = ResponseStatus.Error;
                response.ErrorCode = EnumHelper.ConvertToEnum<ErrorCode>(ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ErrorCode = ErrorCode.INTERNAL_SERVER_ERROR;
                response.Status = ResponseStatus.Error;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }

        public ActionResult<BaseResponse<UsersDBO>> GetUserById(string userId)
        {
            BaseResponse<UsersDBO> response = new BaseResponse<UsersDBO>(ResponseStatus.Fail);

            try
            {
                // If user id not found then return bad request
                if (string.IsNullOrWhiteSpace(userId))
                {
                    throw new BadHttpRequestException(EnumHelper.ConvertToString(ErrorCode.USER_ID_REQUIRED));
                }

                // Get user by user id
                UsersDBO user = _usersRepo.GetUserById(userId);

                // Assign user to data if found else throw bad request
                response.Data = user ?? throw new BadHttpRequestException(EnumHelper.ConvertToString(ErrorCode.USER_NOT_FOUND));

                response.Status = ResponseStatus.Success;
            }
            catch (BadHttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                response.Status = ResponseStatus.Error;
                response.ErrorCode = EnumHelper.ConvertToEnum<ErrorCode>(ex.Message);
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.ErrorCode = ErrorCode.INTERNAL_SERVER_ERROR;
                response.Status = ResponseStatus.Error;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }
    }
}
