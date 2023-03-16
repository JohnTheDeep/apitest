using apitest.Models.Enums;

namespace apitest.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public User(string login, string password, UserType userType)
        {
            Login = login;
            Password = password;
            UserType = userType;
        }
    }
}
