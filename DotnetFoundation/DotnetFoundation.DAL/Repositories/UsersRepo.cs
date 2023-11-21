using DotnetFoundation.DAL.DatabaseContext;
using DotnetFoundation.DAL.Models;
using DotnetFoundation.DAL.Repositories.Interfaces;

namespace DotnetFoundation.DAL.Repositories
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
