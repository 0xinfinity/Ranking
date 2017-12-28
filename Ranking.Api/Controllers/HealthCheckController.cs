using Microsoft.AspNetCore.Mvc;

namespace Ranking.Api.Controllers
{
    [Route("/api/[controller]")]
    public class HealthCheckController : Controller
    {
        [Route("isAlive")]
        public IActionResult IsAlive()
        {
            return Ok("I'm alive!");
        }
    }
}