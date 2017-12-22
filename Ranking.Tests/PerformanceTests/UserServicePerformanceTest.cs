using NUnit.Framework;
using Ranking.DataProviders.Scores;
using Ranking.DataProviders.Users;
using Ranking.Services.Scores;
using Ranking.Services.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ranking.Tests.PerformanceTests
{
    class UserServicePerformanceTest
    {
        private static string _connection = @"Data Source=pc\sqlexpress;Initial Catalog=ranking_database;User ID=ranking_user;Password=ranking;";
        private IUsersService _userService;

        [OneTimeSetUp]
        public void SetUp()
        {
            var userProvider = new UsersDataProvider(_connection);
            var scoreProvider = new ScoreDataProvider(_connection);
            _userService = new UsersService(scoreProvider, userProvider);
        }

        [Test]
        public void CollectFullUserDataPerformanceTest()
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            _userService.CollectFullUserData("Player_341").Wait();

            s.Stop();
            Console.WriteLine(s.ElapsedMilliseconds);

            Assert.IsTrue(s.ElapsedMilliseconds < 100);
        }
    }
}
