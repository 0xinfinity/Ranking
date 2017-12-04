using System.Collections.Generic;
using System.Threading.Tasks;
using Ranking.Models.Scores;
using Ranking.Models.Weapons;

namespace Ranking.Services.Scores
{
    public interface IScoreService
    {
        Task AddUserScore(ScoreDTO score);
    }
}
