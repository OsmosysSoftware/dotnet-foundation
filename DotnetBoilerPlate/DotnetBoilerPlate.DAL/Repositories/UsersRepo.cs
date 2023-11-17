using DotnetBoilerPlate.DAL.DatabaseContext;
using DotnetBoilerPlate.DAL.Models;
using DotnetBoilerPlate.DAL.Repositories.Interfaces;

namespace DotnetBoilerPlate.DAL.Repositories
{
    public class UsersRepo : IUsersRepo
    {
        private readonly SqlDatabaseContext _databaseContext;

        public UsersRepo(SqlDatabaseContext databaseContext) => _databaseContext = databaseContext;

        public List<UsersDBO> GetAllUsers()
        {
            return _databaseContext.Users.ToList();
        }

        public UsersDBO GetUserById(string userId)
        {
            return _databaseContext.Users
                .Where(user => user.UserId == userId)
                .FirstOrDefault();
        }
    }
}
