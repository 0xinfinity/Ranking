using System.Threading.Tasks;
using Ranking.Models.Users;

namespace Ranking.DataProviders.Users
{
    public class UsersDataProvider : IUsersDataProvider
    {
        private readonly string _connectionString;

        public UsersDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<UserDTO> GetBasicUser(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
