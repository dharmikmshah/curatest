using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace CuraGames.Utils
{

    public class CurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CurrentUser> _logger;

        public CurrentUser(IHttpContextAccessor httpContextAccessor, ILogger<CurrentUser> logger)
        {
            _httpContextAccessor= httpContextAccessor;
            _logger= logger;
        }

        public int CurrentUserId
        {
            get
            {
                try
                {
                    var claims = _httpContextAccessor.HttpContext.User.Claims;
                    var uId = claims.Where(p => p.Type == "UserId").Select(p => p.Value).SingleOrDefault();
                    _ = int.TryParse(uId, out int userId);

                    return userId;
                }
                catch (Exception ex)
                {
                    _logger.LogError("{ex}", ex);
                }
                return 0;
            }
        }

        public string CurrentUserName
        {
            get
            {
                try
                {
                    var claims = _httpContextAccessor.HttpContext.User.Claims;
                    return claims.Where(p => p.Type == "UserName").Select(p => p.Value).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    _logger.LogError("{ex}", ex);
                }
                return "";
            }
        }

        public List<string> CurrentUserRegions
        {
            get
            {
                List<string> regions = new List<string>();
                try
                {
                    var claims = _httpContextAccessor.HttpContext.User.Claims;
                    var regionAccess = claims.Where(p => p.Type == "RegionAccess").FirstOrDefault()?.Value;
                    
                    if (!string.IsNullOrEmpty(regionAccess)) { regions = regionAccess.Split(",").ToList(); }
                }
                catch (Exception ex)
                {
                    _logger.LogError("{ex}", ex);
                }
                return regions;
            }
        }
    }
}
