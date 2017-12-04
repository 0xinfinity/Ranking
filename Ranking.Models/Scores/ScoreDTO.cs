using System;

namespace Ranking.Models.Scores
{
    public class ScoreDTO
    {
        public int UserId { get; set; }
        public int KillsCount { get; set; }
        public int DeathsCount { get; set; }
    }
}
