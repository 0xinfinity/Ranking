using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ranking.Api
{
    public static class AppConfiguration
    {
        public static int ItemsPerPage { get; set; }

        static AppConfiguration()
        {
            ItemsPerPage = 10;
        }
    }
}
