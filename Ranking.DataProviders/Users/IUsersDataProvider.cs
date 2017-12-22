using System.Threading.Tasks;
using Ranking.Models.Users;
using System.Collections.Generic;

namespace Ranking.DataProviders.Users
{
    public interface IUsersDataProvider
    {
        Task<UserDTO> GetBasicUser(string nickname);
        Task<UserDTO> CreateUser(string nickname);
        Task<UserWithScore> GetPlayerFullData(string nickname);
        Task<List<UserWithScore>> GetPlayersList(int page,int itemsPerPage);
    }
}
