using NUnit.Framework;
using Ranking.DataProviders.Scores;
using Ranking.DataProviders.Users;
using Ranking.Services.Scores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ranking.Tests.PerformanceTests
{
    [TestFixture]
    public class ScoreServicePerformanceTest
    {
        private static string _connection = @"Data Source=pc\sqlexpress;Initial Catalog=ranking_database;User ID=ranking_user;Password=ranking;";
        private IScoreService _scoreService;

        [OneTimeSetUp]
        public void SetUp()
        {
            var userProvider = new UsersDataProvider(_connection);
            var scoreProvider = new ScoreDataProvider(_connection);
            _scoreService = new ScoreService(scoreProvider, userProvider);
        }

        [Test]
        public void AddSuididePerformanceTest()
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            _scoreService.AddSuicide("test1").Wait();

            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);

            Assert.IsTrue(s.ElapsedMilliseconds < 100);
        }
        [Test]
        public void AddScorePerformanceTest()
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            _scoreService.AddScore("test2", "test3", Models.Weapons.Weapon.Ak47, Models.BodyParts.BodyPart.Arm).Wait();

            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);

            Assert.IsTrue(s.ElapsedMilliseconds < 100);
        }

        [Test]
        public void AddSpawnKillPerformanceTest()
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            _scoreService.AddSpawnKill("test4", "test5", Models.Weapons.Weapon.Ak47, Models.BodyParts.BodyPart.Arm).Wait();

            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);

            Assert.IsTrue(s.ElapsedMilliseconds < 100);
        }

        [Test]
        public void AddTeamKillPerformanceTest()
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            _scoreService.AddTeamKill("test6", "test7", Models.Weapons.Weapon.Ak47, Models.BodyParts.BodyPart.Arm).Wait();

            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);

            Assert.IsTrue(s.ElapsedMilliseconds < 100);
        }
    }
}
