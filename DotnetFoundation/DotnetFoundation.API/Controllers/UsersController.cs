using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using DotnetFoundation.API.BLL.Interfaces;
using DotnetFoundation.API.Models;
using DotnetFoundation.DAL.Models;

namespace DotnetFoundation.API.Controllers
{
    [EnableCors]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersBLL _usersBll;
        public UsersController(IUsersBLL usersBll) => _usersBll = usersBll;

        /// <summary>
        /// Get All users.
        /// </summary>
        /// <remarks> ERROR CODES:
        /// INTERNAL_SERVER_ERROR
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("users")]
        public ActionResult<BaseResponse<List<UsersDBO>>> GetAllUsers()
        {
            return _usersBll.GetAllUsers();
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="userId">user id to fetch</param>
        /// <remarks> ERROR CODES:
        /// USER_ID_REQUIRED, USER_NOT_FOUND, INTERNAL_SERVER_ERROR
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("users/{userId}")]
        public ActionResult<BaseResponse<UsersDBO>> GetUserById(string userId)
        {
            return _usersBll.GetUserById(userId);
        }
    }
}
