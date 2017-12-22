using System.Threading.Tasks;
using Ranking.Models.BodyParts;
using Ranking.Models.Weapons;
using Ranking.DataProviders.Scores;
using Ranking.DataProviders.Users;

namespace Ranking.Services.Scores
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreDataProvider _scoreDataProvider;
        private readonly IUsersDataProvider _usersDataProvider;

        public ScoreService(IScoreDataProvider scoreDataProvider, IUsersDataProvider usersDataProvider)
        {
            _scoreDataProvider = scoreDataProvider;
            _usersDataProvider = usersDataProvider;
        }

        public async Task AddScore(string killerNickname, string victimNickname, Weapon weapon, BodyPart bodyPart)
        {
            await CreatePlayerIfNotExists(killerNickname);
            await CreatePlayerIfNotExists(victimNickname);

            await _scoreDataProvider.AddWeaponKill(killerNickname, weapon);
            await _scoreDataProvider.AddBodyPartKill(killerNickname, bodyPart);

            await _scoreDataProvider.AddWeaponDeath(victimNickname, weapon);
            await _scoreDataProvider.AddBodyPartDeath(victimNickname, bodyPart);
        }

        public async Task AddSpawnKill(string killerNickname, string victimNickname, Weapon weapon, BodyPart bodyPart)
        {
            await CreatePlayerIfNotExists(killerNickname);
            await CreatePlayerIfNotExists(victimNickname);

            await _scoreDataProvider.AddWeaponKill(killerNickname, weapon, true);
            await _scoreDataProvider.AddBodyPartKill(killerNickname, bodyPart);

            await _scoreDataProvider.AddWeaponDeath(victimNickname, weapon);
            await _scoreDataProvider.AddBodyPartDeath(victimNickname, bodyPart);
        }

        public async Task AddSuicide(string nickname)
        {
            await CreatePlayerIfNotExists(nickname);
            await _scoreDataProvider.AddSuicide(nickname);
        }

        public async Task AddTeamKill(string killerNickname, string victimNickname, Weapon weapon, BodyPart bodyPart)
        {
            await AddSpawnKill(killerNickname, victimNickname, weapon, bodyPart);
        }

        private async Task CreatePlayerIfNotExists(string nickname)
        {
            var player = await _usersDataProvider.GetBasicUser(nickname);

            if (player == null)
            {
                player = await _usersDataProvider.CreateUser(nickname);
            }
        }

    }
}
