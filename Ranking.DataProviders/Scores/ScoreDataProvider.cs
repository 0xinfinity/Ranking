using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ranking.Models.BodyParts;
using Ranking.Models.Scores;
using Ranking.Models.Weapons;
using Dapper;
using System.Data.SqlClient;
using Ranking.Models.Users;
using System.Linq;

namespace Ranking.DataProviders.Scores
{
    public class ScoreDataProvider : IScoreDataProvider
    {
        private readonly string _connectionString;

        public ScoreDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddBodyPartDeath(string nickname, BodyPart bodyPart)
        {
            await CreateUsersBodyPartRecordIfNotExists(nickname, bodyPart);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(@"
                   UPDATE [UsersBodyPartsScores] 
                    SET DeathsCount = DeathsCount + 1
                    WHERE Nickname=@nickname AND BodyPartId=@bodyPart", new
                {
                    nickname,
                    bodyPart,
                });
            }

        }

        public async Task AddBodyPartKill(string nickname, BodyPart bodyPart)
        {
            await CreateUsersBodyPartRecordIfNotExists(nickname, bodyPart);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(@"
                   UPDATE [UsersBodyPartsScores] 
                    SET KillsCount = KillsCount + 1
                    WHERE Nickname=@nickname AND BodyPartId=@bodyPart", new
                {
                    nickname,
                    bodyPart,
                });
            }
        }

        public async Task AddSuicide(string nickname)
        {
            await CreateUsersWeaponsRecordIfNotExists(nickname, Weapon.Suicide);

            await AddWeaponDeath(nickname, Weapon.Suicide);
        }

        public async Task AddWeaponDeath(string nickname, Weapon weapon)
        {
            await CreateUsersWeaponsRecordIfNotExists(nickname, weapon);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(@"
                   UPDATE [UsersWeaponsScores] 
                    SET DeathsCount = DeathsCount + 1
                    WHERE Nickname=@nickname AND WeaponId=@weapon", new
                {
                    nickname,
                    weapon,
                });
            }

        }

        public async Task AddWeaponKill(string nickname, Weapon weapon, bool isSpawnOrTeamKill = false)
        {
            await CreateUsersWeaponsRecordIfNotExists(nickname, weapon);

            int scoreToAdd = isSpawnOrTeamKill ? -1 : 1;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(@"
                   UPDATE [UsersWeaponsScores] 
                    SET KillsCount = KillsCount + @scoreToAdd
                    WHERE Nickname=@nickname AND WeaponId=@weapon", new
                {
                    nickname,
                    weapon,
                    scoreToAdd
                });
            }
        }

        public async Task<List<UsersBodyPartsScoreDTO>> GetUserBodyPartsScore(string nickname)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<UsersBodyPartsScoreDTO>(@"
                    SELECT * FROM [dbo].[UsersBodyPartsScores] WHERE Nickname = @nickname", new
                {
                    nickname,
                });

                return result.ToList();
            }
        }

        public async Task<List<UsersWeaponsScoreDTO>> GetUserWeaponsScore(string nickname)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<UsersWeaponsScoreDTO>(@"
                    SELECT * FROM [dbo].[UsersWeaponsScores] WHERE Nickname = @nickname", new
                {
                    nickname,
                });

                return result.ToList();
            }
        }
        private async Task CreateUsersWeaponsRecordIfNotExists(string nickname, Weapon weapon)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(@"
                    BEGIN
                       IF NOT EXISTS (SELECT * FROM [UsersWeaponsScores] 
                                       WHERE Nickname = @nickname
                                       AND WeaponId = @weapon)
                       BEGIN
                           INSERT INTO [UsersWeaponsScores] (Nickname, WeaponId, KillsCount, DeathsCount)
                           VALUES (@nickname, @weapon, 0, 0)
                       END
                    END", new
                {
                    nickname,
                    weapon,
                });
            }
        }
        private async Task CreateUsersBodyPartRecordIfNotExists(string nickname, BodyPart bodyPart)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(@"
                    BEGIN
                       IF NOT EXISTS (SELECT * FROM [UsersBodyPartsScores] 
                                       WHERE nickname = @nickname
                                       AND BodyPartId = @bodyPart)
                       BEGIN
                           INSERT INTO [UsersBodyPartsScores] (Nickname, BodyPartId, KillsCount, DeathsCount)
                           VALUES (@nickname, @bodyPart, 0, 0)
                       END
                    END", new
                {
                    nickname,
                    bodyPart,
                });
            }
        }
    }
}
