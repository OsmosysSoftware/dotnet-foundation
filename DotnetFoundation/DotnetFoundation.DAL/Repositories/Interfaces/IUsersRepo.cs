using DotnetFoundation.DAL.Models;

namespace DotnetFoundation.DAL.Repositories.Interfaces
{
    public interface IUsersRepo
    {
        List<UsersDBO> GetAllUsers();
        UsersDBO GetUserById(string userId);
    }
}
