using CuraGames.Models;

namespace CuraGames.Interface
{
    public interface IAuth
    {
        public UserInfo Authenticate(LoginModel model);
    }
}
