using Microsoft.AspNetCore.Mvc;
using DotnetFoundation.API.Models;
using DotnetFoundation.DAL.Models;

namespace DotnetFoundation.API.BLL.Interfaces
{
    public interface IUsersBLL
    {
        ActionResult<BaseResponse<List<UsersDBO>>> GetAllUsers();
        ActionResult<BaseResponse<UsersDBO>> GetUserById(string userId);
    }
}
