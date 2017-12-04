using System.Threading.Tasks;
using Ranking.Models.Users;

namespace Ranking.DataProviders.Users
{
    public interface IUsersDataProvider
    {
        Task<UserDTO> GetBasicUser(int userId);
    }
}
