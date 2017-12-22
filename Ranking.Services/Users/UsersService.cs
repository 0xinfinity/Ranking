using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ranking.DataProviders.Scores;
using Ranking.DataProviders.Users;
using Ranking.Models.Scores;
using Ranking.Models.Users;
using Ranking.Models.Weapons;

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


        public async Task<UserWithScore> CollectFullUserData(string nickname)
        {
            var bodyParts = await _scoreDataProvider.GetUserBodyPartsScore(nickname);
            var weapons = await _scoreDataProvider.GetUserWeaponsScore(nickname);

            //testy porównawcze obu rozwiązań? +naprawa unit testu
            //metoda do pobierania paginowanej listy graczy

            // var user = await _userDataProvider.GetBasicUser(nickname);
            var full = await _userDataProvider.GetPlayerFullData(nickname);
            //var full = new UserWithScore(user);
            //full.Kills = weapons.Sum(s => s.KillsCount);
            //full.Deaths = weapons.Sum(s => s.DeathsCount);
            full.RatioKillsDeaths = Math.Round(((float)full.Kills) / ((float)full.Deaths), 2);
            full.RatioKillsPerMinute = Math.Round(((float)full.Kills) / ((float)full.OnlineTime), 2);
            full.IsOnline = full.LastActivityDate.AddMinutes(5) > DateTime.Now;//5 minut
            //full.FavoriteWeapon = GetFavoriteWeapon(weapons);
            //full.Position = 1;

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

        public async Task<List<UserWithScore>> GetPlayersList(int page, int itemsPerPage)
        {
            return await _userDataProvider.GetPlayersList(page, itemsPerPage);
        }
    }
}
