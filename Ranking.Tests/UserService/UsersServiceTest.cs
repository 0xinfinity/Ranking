using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Ranking.DataProviders.Scores;
using Ranking.DataProviders.Users;
using Ranking.Models.Scores;
using Ranking.Models.Users;
using Ranking.Models.Weapons;
using Ranking.Services.Scores;
using Ranking.Services.Users;

namespace Ranking.Tests.UserService
{
    [TestFixture]
    public partial class UsersServiceTest
    {
        private Mock<IScoreDataProvider> _scoreDataProviderMock;
        private Mock<IUsersDataProvider> _usersDataProviderMock;


        [Test]
        public async Task CollectFullUserDataTest()
        {
            Setup();
            var sut = new UsersService(_scoreDataProviderMock.Object, _usersDataProviderMock.Object);

            var result = await sut.CollectFullUserData(1);
            Assert.AreEqual(75, result.Kills);
            Assert.AreEqual(50, result.Deaths);
            Assert.AreEqual(Weapon.Deagle, result.FavoriteWeapon);
            Assert.AreEqual(1.50, result.RatioKillsDeaths);
            Assert.AreEqual(0.75, result.RatioKillsPerMinute);
            Assert.AreEqual(1, result.Position);
        }

        [Test]
        public void FavoriteWeaponTest()
        {
            var sut = new UsersService(_scoreDataProviderMock.Object, _usersDataProviderMock.Object);
            List<UsersWeaponsScoreDTO> list = new List<UsersWeaponsScoreDTO>
            {
                new UsersWeaponsScoreDTO
                {
                    Weapon = Weapon.Ak47,
                    UserId = 1,
                    KillsCount = 100,
                    DeathsCount = 200,
                },
                new UsersWeaponsScoreDTO
                {
                    Weapon = Weapon.Deagle,
                    UserId = 1,
                    KillsCount = 100,
                    DeathsCount = 200,
                },
                new UsersWeaponsScoreDTO
                {
                    Weapon = Weapon.M82A1,
                    UserId = 1,
                    KillsCount = 100,
                    DeathsCount = 199,
                },
            };
            var result = sut.GetFavoriteWeapon(list);

            Assert.AreEqual(Weapon.M82A1, result);
        }
        [Test]
        public void FavoriteWeaponTestNull()
        {
            var sut = new UsersService(_scoreDataProviderMock.Object, _usersDataProviderMock.Object);
            List<UsersWeaponsScoreDTO> list = new List<UsersWeaponsScoreDTO>();

            var result = sut.GetFavoriteWeapon(list);

            Assert.AreEqual(Weapon.Knife, result);
        }
    }
}
