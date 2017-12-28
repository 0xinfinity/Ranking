using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ranking.Services.Users;

namespace Ranking.Api.Controllers
{
    [Route("/api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly IUsersService _usersService;
        public PlayerController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [Route("list/{page:int}")]
        public async Task<IActionResult> Index(int page = 0)
        {
            var players = await _usersService.GetPlayersList(page, AppConfiguration.ItemsPerPage);
            return Ok(players);
        }
    }
}