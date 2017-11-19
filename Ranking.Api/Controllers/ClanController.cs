using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ranking.Api.Controllers
{
    public class ClanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}