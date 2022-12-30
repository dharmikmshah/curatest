using CuraGames.Enums;
using CuraGames.Interface;
using CuraGames.Models;
using CuraGames.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
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
        private readonly CurrentUser _cuser;


        public UserController(IConfiguration config, IAuth iAuth, IUsers iuser, CurrentUser user)
        {
            _configuration = config;
            _iAuth = iAuth;
            _iuser = iuser;
            _cuser = user;
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
                        new Claim(ClaimTypes.Role,user.UserRole),
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


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("profile")]
        public ActionResult Profile()
        {
            return Ok(_iuser.GetUser(_cuser.CurrentUserName));
        }
    }
}
