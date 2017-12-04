using System;
using System.Collections.Generic;
using Moq;
using Ranking.DataProviders.Scores;
using Ranking.DataProviders.Users;
using Ranking.Models.BodyParts;
using Ranking.Models.Scores;
using Ranking.Models.Users;
using Ranking.Models.Weapons;

namespace Ranking.Tests.UserService
{
    public partial class UsersServiceTest
    {
        void Setup()
        {
            _scoreDataProviderMock = new Mock<IScoreDataProvider>();
            _usersDataProviderMock = new Mock<IUsersDataProvider>();
            _scoreDataProviderMock = new Mock<ScoreDataProvider>("Test").As<IScoreDataProvider>();
            _usersDataProviderMock = new Mock<UsersDataProvider>("Test").As<IUsersDataProvider>();

            _scoreDataProviderMock.Setup(s => s.GetUserBodyPartsScore(1))
                .ReturnsAsync(() =>
                {
                    var list = new List<UsersBodyPartsScoreDTO>();
                    list.Add(new UsersBodyPartsScoreDTO
                    {
                        BodyPart = BodyPart.Arm,
                        KillsCount = 20,
                        UserId = 1,
                        DeathsCount = 0
                    });
                    list.Add(new UsersBodyPartsScoreDTO
                    {
                        BodyPart = BodyPart.Back,
                        KillsCount = 30,
                        UserId = 1,
                        DeathsCount = 20
                    });
                    list.Add(new UsersBodyPartsScoreDTO
                    {
                        BodyPart = BodyPart.Leg,
                        KillsCount = 0,
                        UserId = 1,
                        DeathsCount = 20
                    });
                    list.Add(new UsersBodyPartsScoreDTO
                    {
                        BodyPart = BodyPart.Head,
                        KillsCount = 20,
                        UserId = 1,
                        DeathsCount = 10
                    });
                    return list;
                });

            _scoreDataProviderMock.Setup(s => s.GetUserWeaponsScore(1))
                .ReturnsAsync(() =>
                {
                    var list = new List<UsersWeaponsScoreDTO>();
                    list.Add(new UsersWeaponsScoreDTO
                    {
                        Weapon = Weapon.Ak47,
                        KillsCount = 20,
                        UserId = 1,
                        DeathsCount = 0
                    });
                    list.Add(new UsersWeaponsScoreDTO
                    {
                        Weapon = Weapon.Deagle,
                        KillsCount = 35,
                        UserId = 1,
                        DeathsCount = 20
                    });
                    list.Add(new UsersWeaponsScoreDTO
                    {
                        Weapon = Weapon.Grenade,
                        KillsCount = 0,
                        UserId = 1,
                        DeathsCount = 10
                    });
                    list.Add(new UsersWeaponsScoreDTO
                    {
                        Weapon = Weapon.FnMinimi,
                        KillsCount = 20,
                        UserId = 1,
                        DeathsCount = 20
                    });
                    return list;
                });


            _usersDataProviderMock.Setup(s => s.GetBasicUser(1))
                .ReturnsAsync(() => new UserDTO()
                {
                    LastActivityDate = DateTime.Now,
                    Id = 1,
                    OnlineTime = 100,
                    NickName = "TestPlayer",
                    UserType = UserType.StandardUser,
                    IsDeleted = false,
                    IsBlocked = false,
                    CreateDate = DateTime.Now
                });
        }

    }
}