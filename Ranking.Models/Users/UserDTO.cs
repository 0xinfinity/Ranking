using System;

namespace Ranking.Models.Users
{
    public class UserDTO
    {
        public int ClanId { get; set; }
        public string NickName { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreateDate { get; set; }
        public int OnlineTime { get; set; }
        public DateTime LastActivityDate { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
    }
}
