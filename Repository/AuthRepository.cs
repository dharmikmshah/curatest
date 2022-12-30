using CuraGames.Interface;
using CuraGames.Models;

namespace CuraGames.Repository
{
    public class AuthRepository : IAuth
    {
        private readonly IUsers _iUsers;
        private ILogger<AuthRepository> _logger;

        public AuthRepository(IUsers iusers, ILogger<AuthRepository> logger)
        {
            _iUsers = iusers;
            _logger = logger;
        }

        public UserInfo Authenticate(LoginModel model)
        {
            try
            {
                var user = _iUsers.GetUser(model.UserName);
                if (user != null && user.Password == model.Password)
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, null, null);
            }
            return null;
        }

    }
}
