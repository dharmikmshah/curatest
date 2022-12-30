using CuraGames.Interface;
using CuraGames.Models;

namespace CuraGames.Repository
{
    public class AuthRepository : IAuth
    {
        private readonly IUsers _iUsers;

        public AuthRepository(IUsers iusers)
        {
            _iUsers = iusers;
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
                throw;
            }
            return null;
        }

    }
}
