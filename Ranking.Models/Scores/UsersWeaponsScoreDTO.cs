using System;
using System.Collections.Generic;
using System.Text;
using Ranking.Models.Weapons;

namespace Ranking.Models.Scores
{
    public class UsersWeaponsScoreDTO : ScoreDTO
    {
        public Weapon Weapon { get; set; }
    }
}
