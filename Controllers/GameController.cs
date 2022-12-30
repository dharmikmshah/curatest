using CuraGames.Interface;
using CuraGames.Utils;
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
        private readonly CurrentUser _cuser;


        public GameController(IGames iGames, CurrentUser currentUser)
        {
            _iGames = iGames;
            _cuser = currentUser;
        }

        [Authorize]
        [HttpGet("getavailablegames")]
        public ActionResult GetAvailableGames()
        {
            return Ok(_iGames.GetGamesByRegion(_cuser.CurrentUserRegions));
        }
    }
}
