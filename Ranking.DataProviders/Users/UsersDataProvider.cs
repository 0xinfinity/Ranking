using System.Threading.Tasks;
using Ranking.Models.Users;
using System;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Ranking.DataProviders.Users
{
    public class UsersDataProvider : IUsersDataProvider
    {
        private readonly string _connectionString;

        public UsersDataProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserDTO> CreateUser(string nickname)
        {
            var userType = UserType.StandardUser;
            var dateNow = DateTime.Now;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(@"INSERT INTO Users VALUES (@nickname, null, @userType, @dateNow, 'false', 'false', @dateNow)", new
                {
                    nickname,
                    userType,
                    dateNow
                });
            }
            return await GetBasicUser(nickname);
        }

        public async Task<UserDTO> GetBasicUser(string nickname)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<UserDTO>(@"SELECT * FROM Users WHERE Nickname = @nickname", new
                {
                    nickname
                });
                return result.FirstOrDefault();
            }
        }

        public async Task<UserWithScore> GetPlayerFullData(string nickname)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<UserWithScore>(@"
                    BEGIN
                    DECLARE  @nick as varchar(max) ;
                    SET @nick= @nickname;

                    Declare @k as int
                    set @k = (Select top 1 SUM(w.KillsCount) from dbo.UsersWeaponsScores w where nickname=@nick group by w.WeaponId )

                    Declare @d as int
                    set @d = (Select top 1 SUM(w.DeathsCount) from dbo.UsersWeaponsScores w where nickname=@nick group by w.WeaponId )

                    Select * from
                    (
                    SELECT 
	                       u.[Nickname]
                          ,u.[ClanId]
                          ,u.[UserType]
                          ,u.[CreateDate]
                          ,u.[IsBlocked]
                          ,u.[IsDeleted]
                          ,u.[LastActivityDate]
	                    --  ,u.[OnlineTime]
	                      ,(Select top 1 w.WeaponId from dbo.UsersWeaponsScores w where nickname=@nick order by	w.KillsCount ,w.DeathsCount ) as FavoriteWeaponId
	                      ,@k as Kills
	                      ,@d as Deaths
	                      ,ROW_NUMBER()OVER ( ORDER BY ((@k-@d)*0.7) desc) as Position

		
                      FROM [Users] u

                      ) as Result
                      where nickname=@nick
                      END


                ", new
                {
                    nickname
                });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<UserWithScore>> GetPlayersList(int page, int itemsPerPage)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<UserWithScore>(@"
                    BEGIN
                    Declare @k as int
                    set @k = (Select top 1 SUM(w.KillsCount) from dbo.UsersWeaponsScores w  group by w.WeaponId )

                    Declare @d as int
                    set @d = (Select top 1 SUM(w.DeathsCount) from dbo.UsersWeaponsScores w group by w.WeaponId )

					Declare @count as int
					set @count = @ItemsPerPage;

					Declare @page as int
					set @page = @p;

                    Select * from
                    (
                    SELECT 
	                       u.[Nickname]
                          ,u.[ClanId]
                          ,u.[UserType]
                          ,u.[CreateDate]
                          ,u.[IsBlocked]
                          ,u.[IsDeleted]
                          ,u.[LastActivityDate]
	                    --  ,u.[OnlineTime]
	                      ,(Select top 1 w.WeaponId from dbo.UsersWeaponsScores w order by	w.KillsCount ,w.DeathsCount ) as FavoriteWeaponId
	                      ,@k as Kills
	                      ,@d as Deaths
	                      ,ROW_NUMBER()OVER ( ORDER BY ((@k-@d)*0.7) desc) as Position

		
                      FROM [Users] u

                      ) as Result
					  WHERE   Position >= @page*@count  AND Position < (@page*@count)+@count
ORDER BY Position
                    END


                ", new
                {
                    p = page,
                    ItemsPerPage = itemsPerPage
                });
                return result.ToList();
            }
        }
    }
}
