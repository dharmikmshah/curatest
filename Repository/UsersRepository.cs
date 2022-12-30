using CuraGames.Enums;
using CuraGames.Interface;
using CuraGames.Models;

namespace CuraGames.Repository
{
    public class UsersRepository : IUsers
    {
        #region Private Methods
        List<UserInfo> _users = new List<UserInfo>()
        {
            new UserInfo(){
                FirstName = "Dharmik",
                LastName="Admin",
                UserId=1,
                Password="Admin@123",
                Username="admin",
                UserRole=UserRole.Admin.ToString(),
                RegionsAccess = new List<string>{GameRegion.Board.ToString(),GameRegion.Vip.ToString()}
            },
            new UserInfo(){
                FirstName = "Player",
                LastName="Board",
                UserId=1,
                Password="Player@123",
                Username="playerboard",
                UserRole=UserRole.Player.ToString(),
                RegionsAccess = new List<string>{GameRegion.Board.ToString()}
            },
            new UserInfo(){
                FirstName = "Player",
                LastName="VIP",
                UserId=1,
                Password="Player@123",
                Username="playervip",
                UserRole=UserRole.Player.ToString(),
                RegionsAccess = new List<string>{GameRegion.Board.ToString(), GameRegion.Vip.ToString() }
            },
        };
        #endregion

        #region Public Methods
        public UserInfo GetUser(string username)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    return _users.Where(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        #endregion
    }
}
