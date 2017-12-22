using System;
using System.Collections.Generic;
using System.Text;
using Ranking.Models.Weapons;

namespace Ranking.Models.Users
{
    public class UserWithScore : UserDTO
    {
        public UserWithScore()
        {

        }
        public UserWithScore(UserDTO user)
        {
            this.NickName = user.NickName;
            this.CreateDate = user.CreateDate;
            this.IsBlocked = user.IsBlocked;
            this.UserType = user.UserType;
            this.IsDeleted = user.IsDeleted;
            this.OnlineTime = user.OnlineTime;
            this.LastActivityDate = user.LastActivityDate;
        }

        public int Position { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public double RatioKillsDeaths { get; set; }
        public double RatioKillsPerMinute { get; set; }
        public Weapon FavoriteWeapon { get; set; }
        public bool IsOnline { get; set; }
    }
}
