using System.Collections.Generic;
using System.Threading.Tasks;
using Ranking.Models.Scores;

namespace Ranking.DataProviders.Scores
{
    public interface IScoreDataProvider
    {
        Task<List<UsersBodyPartsScoreDTO>> GetUserBodyPartsScore(int userId);
        Task<List<UsersWeaponsScoreDTO>> GetUserWeaponsScore(int userId);

    }
}
