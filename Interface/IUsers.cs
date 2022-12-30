using CuraGames.Models;

namespace CuraGames.Interface
{
    public interface IUsers
    {
        public UserInfo GetUser(string username);
    }
}
