using Ranking.DataProviders.Scores;
using Ranking.DataProviders.Users;
using System;
using System.Threading.Tasks;
using Ranking.Models.Weapons;
using Ranking.Models.BodyParts;
using Ranking.Services.Scores;
using System.Diagnostics;
using System.Collections.Generic;

namespace TestDataGenerator
{
    class Program
    {
        private static string _connection = @"Data Source=pc\sqlexpress;Initial Catalog=ranking_database;User ID=ranking_user;Password=ranking;";
        private static Random _rand = new Random();
        static void Main(string[] args)
        {
            var userProvider = new UsersDataProvider(_connection);
            var scoreProvider = new ScoreDataProvider(_connection);
            var scoreService = new ScoreService(scoreProvider, userProvider);

            Stopwatch watch = new Stopwatch();
            watch.Start();



            for (int i = 0; i < 100; i++)
            {
                scoreService.AddScore($"Player_{i}", $"Victim_{i}", getRandWeapon(), getRandBodyPart()).Wait();
                for (int j = 0; j < 10 + i; j++)
                {
                    scoreService.AddSuicide("Nub").Wait();
                    scoreService.AddTeamKill($"Player_{i}", $"Victim_{i}", getRandWeapon(), getRandBodyPart()).Wait();
                    scoreService.AddSpawnKill($"Player_{i}", $"Victim_{i}", getRandWeapon(), getRandBodyPart()).Wait();
                }
            }




            watch.Stop();

            Console.WriteLine($"Done\nTime elapsed: {watch.Elapsed}");
            Console.Read();
        }

        private static BodyPart getRandBodyPart()
        {
            return (BodyPart)_rand.Next(0, 4);
        }

        private static Weapon getRandWeapon()
        {
            return (Weapon)_rand.Next(0, 7);
        }
    }
}
