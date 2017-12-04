using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ranking.Models.Scores;

namespace Ranking.DataProviders.Scores
{
    public class ScoreDataProvider:IScoreDataProvider
    {
        private readonly string _connectionString;

        public ScoreDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<List<UsersBodyPartsScoreDTO>> GetUserBodyPartsScore(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UsersWeaponsScoreDTO>> GetUserWeaponsScore(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
