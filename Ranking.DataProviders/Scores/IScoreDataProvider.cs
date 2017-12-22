using System.Collections.Generic;
using System.Threading.Tasks;
using Ranking.Models.Scores;
using Ranking.Models.BodyParts;
using Ranking.Models.Weapons;

namespace Ranking.DataProviders.Scores
{
    public interface IScoreDataProvider
    {
        Task<List<UsersBodyPartsScoreDTO>> GetUserBodyPartsScore(string nickname);
        Task<List<UsersWeaponsScoreDTO>> GetUserWeaponsScore(string nickname);
        Task AddWeaponKill(string killerNickname, Weapon weapon, bool isSpawnOrTeamKill = false);
        Task AddBodyPartKill(string killerNickname, BodyPart bodyPart);
        Task AddWeaponDeath(string victimNickname, Weapon weapon);
        Task AddBodyPartDeath(string victimNickname, BodyPart bodyPart);
        Task AddSuicide(string nickname);
    }
}
