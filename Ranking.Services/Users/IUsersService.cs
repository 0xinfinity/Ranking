using System.Threading.Tasks;
using Ranking.Models.Users;

namespace Ranking.Services.Users
{
    public interface IUsersService
    {
        Task<UserWithScore> CollectFullUserData(int userId);
    }
}
