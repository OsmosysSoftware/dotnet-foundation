using Microsoft.AspNetCore.Mvc;
using DotnetBoilerPlate.API.Models;
using DotnetBoilerPlate.DAL.Models;

namespace DotnetBoilerPlate.API.BLL.Interfaces
{
    public interface IUsersBLL
    {
        ActionResult<BaseResponse<List<UsersDBO>>> GetAllUsers();
        ActionResult<BaseResponse<UsersDBO>> GetUserById(string userId);
    }
}
