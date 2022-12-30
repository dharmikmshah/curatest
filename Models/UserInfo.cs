namespace CuraGames.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public List<string> RegionsAccess { get; set; }
    }
}
