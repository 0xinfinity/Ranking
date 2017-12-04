using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Ranking.DataProviders.Scores;
using Ranking.DataProviders.Users;
using Ranking.Models.Scores;
using Ranking.Models.Users;
using Ranking.Models.Weapons;
using Ranking.Services.Scores;

namespace Ranking.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IScoreDataProvider _scoreDataProvider;
        private readonly IUsersDataProvider _userDataProvider;

        public UsersService(IScoreDataProvider scoreDataProvider, IUsersDataProvider userDataProvider)
        {
            _scoreDataProvider = scoreDataProvider;
            _userDataProvider = userDataProvider;
        }


        public async Task<UserWithScore> CollectFullUserData(int userId)
        {
            var bodyParts = await _scoreDataProvider.GetUserBodyPartsScore(userId);
            var weapons = await _scoreDataProvider.GetUserWeaponsScore(userId);

            var user = await _userDataProvider.GetBasicUser(userId);

            var full = new UserWithScore(user);
            full.Kills = weapons.Sum(s => s.KillsCount);
            full.Deaths = weapons.Sum(s => s.DeathsCount);
            full.RatioKillsDeaths = Math.Round(((float)full.Kills) / ((float)full.Deaths), 2);
            full.RatioKillsPerMinute = Math.Round(((float)full.Kills) / ((float)full.OnlineTime), 2);
            full.IsOnline = user.LastActivityDate.AddMinutes(5) > DateTime.Now;//5 minut
            full.FavoriteWeapon = GetFavoriteWeapon(weapons);
            full.Position = 1;

            return full;
        }

        public Weapon GetFavoriteWeapon(List<UsersWeaponsScoreDTO> weapons)
        {
            var scoreDto = weapons
                .OrderByDescending(w => w.KillsCount)
                .ThenBy(w => w.DeathsCount)
                .FirstOrDefault();

            if (scoreDto != null)
            {
                return scoreDto.Weapon;
            }
            return Weapon.Knife;
        }
    }
}
