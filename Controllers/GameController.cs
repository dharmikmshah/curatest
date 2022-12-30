using CuraGames.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;

namespace CuraGames.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGames _iGames;


        public GameController(IGames iGames)
        {
            _iGames = iGames;
        }

        [Authorize]
        [HttpGet("getavailablegames")]
        public ActionResult GetAvailableGames()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var regionAccess = claims.Where(p => p.Type == "RegionAccess").FirstOrDefault()?.Value;
                List<string> regions = new List<string>();
                if (!string.IsNullOrEmpty(regionAccess)) { regions = regionAccess.Split(",").ToList(); }
                return Ok(_iGames.GetGamesByRegion(regions));
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
    }
}
