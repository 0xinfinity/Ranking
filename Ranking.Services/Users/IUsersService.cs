using System.Threading.Tasks;
using Ranking.Models.Users;
using System.Collections.Generic;

namespace Ranking.Services.Users
{
    public interface IUsersService
    {
        Task<UserWithScore> CollectFullUserData(string nickname);
        Task<List<UserWithScore>> GetPlayersList(int page, int itemsPerPage);
    }
}
