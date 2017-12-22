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

            _scoreDataProviderMock.Setup(s => s.GetUserBodyPartsScore("Player"))
                .ReturnsAsync(() =>
                {
                    var list = new List<UsersBodyPartsScoreDTO>();
                    list.Add(new UsersBodyPartsScoreDTO
                    {
                        BodyPart = BodyPart.Arm,
                        KillsCount = 20,
                        Nickname = "Player",
                        DeathsCount = 0
                    });
                    list.Add(new UsersBodyPartsScoreDTO
                    {
                        BodyPart = BodyPart.Back,
                        KillsCount = 30,
                        Nickname = "Player",
                        DeathsCount = 20
                    });
                    list.Add(new UsersBodyPartsScoreDTO
                    {
                        BodyPart = BodyPart.Leg,
                        KillsCount = 0,
                        Nickname = "Player",
                        DeathsCount = 20
                    });
                    list.Add(new UsersBodyPartsScoreDTO
                    {
                        BodyPart = BodyPart.Head,
                        KillsCount = 20,
                        Nickname = "Player",
                        DeathsCount = 10
                    });
                    return list;
                });

            _scoreDataProviderMock.Setup(s => s.GetUserWeaponsScore("Player"))
                .ReturnsAsync(() =>
                {
                    var list = new List<UsersWeaponsScoreDTO>();
                    list.Add(new UsersWeaponsScoreDTO
                    {
                        Weapon = Weapon.Ak47,
                        KillsCount = 20,
                        Nickname = "Player",
                        DeathsCount = 0
                    });
                    list.Add(new UsersWeaponsScoreDTO
                    {
                        Weapon = Weapon.Deagle,
                        KillsCount = 35,
                        Nickname = "Player",
                        DeathsCount = 20
                    });
                    list.Add(new UsersWeaponsScoreDTO
                    {
                        Weapon = Weapon.Grenade,
                        KillsCount = 0,
                        Nickname = "Player",
                        DeathsCount = 10
                    });
                    list.Add(new UsersWeaponsScoreDTO
                    {
                        Weapon = Weapon.FnMinimi,
                        KillsCount = 20,
                        Nickname = "Player",
                        DeathsCount = 20
                    });
                    return list;
                });


            _usersDataProviderMock.Setup(s => s.GetBasicUser("Player"))
                .ReturnsAsync(() => new UserDTO()
                {
                    LastActivityDate = DateTime.Now,
                    OnlineTime = 100,
                    NickName = "Player",
                    UserType = UserType.StandardUser,
                    IsDeleted = false,
                    IsBlocked = false,
                    CreateDate = DateTime.Now
                });

            _usersDataProviderMock.Setup(s => s.GetPlayerFullData("Player"))
               .ReturnsAsync(() => new UserWithScore()
               {
                   LastActivityDate = DateTime.Now,
                   OnlineTime = 100,
                   NickName = "Player",
                   UserType = UserType.StandardUser,
                   IsDeleted = false,
                   IsBlocked = false,
                   CreateDate = DateTime.Now,
                   Position = 1,
                   Deaths = 50,
                   Kills = 75,
                   FavoriteWeapon = Weapon.Deagle
               });
        }

    }
}