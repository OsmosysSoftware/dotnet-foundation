using DotnetBoilerPlate.DAL.Models;

namespace DotnetBoilerPlate.DAL.Repositories.Interfaces
{
    public interface IUsersRepo
    {
        List<UsersDBO> GetAllUsers();
        UsersDBO GetUserById(string userId);
    }
}
