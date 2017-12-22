using System.Collections.Generic;
using System.Threading.Tasks;
using Ranking.Models.Scores;
using Ranking.Models.Weapons;
using Ranking.Models.BodyParts;

namespace Ranking.Services.Scores
{
    public interface IScoreService
    {
        Task AddScore(string killerNickname, string victimNickname, Weapon weapon, BodyPart bodyPart);
        Task AddSuicide(string nickname);
        Task AddTeamKill(string killerNickname, string victimNickname, Weapon weapon, BodyPart bodyPart);
        Task AddSpawnKill(string killerNickname, string victimNickname, Weapon weapon, BodyPart bodyPart);
    }
}
