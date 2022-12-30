using CuraGames.Interface;
using CuraGames.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace CuraGames.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private IConfiguration _configuration;
        private readonly IAuth _iAuth;
        private readonly IUsers _iuser;


        public UserController(IConfiguration config, IAuth iAuth, IUsers iuser)
        {
            _configuration = config;
            _iAuth = iAuth;
            _iuser = iuser;
        }

        // POST api/user/login
        [HttpPost("login")]
        public ActionResult Login(LoginModel model)
        {
            var user = _iAuth.Authenticate(model);
            if (user == null)
            {
                return BadRequest("Invalid Credentials");
            }
            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName",$"{user.LastName},{user.FirstName}"),
                        new Claim("UserName", user.Username),
                        new Claim("Role",user.UserRole),
                        new Claim("RegionAccess",string.Join(",", user.RegionsAccess)),
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:Timeout"])),
                signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }


        [Authorize]
        [HttpPost("getall")]
        public ActionResult GetAll()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "UserName").FirstOrDefault()?.Value;
                return Ok(_iuser.GetUser(name));
            }
            else
            {
                return BadRequest("Invalid request");
            }
        }
    }
}
