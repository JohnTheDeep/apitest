using apitest.Models.Enums;

namespace apitest.Models
{
    public class JwtPerson
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserType Role { get; set; }
        public JwtPerson(string login, string password, UserType role)
        {
            Login = login;
            Password = password;
            Role = role;
        }
    }
}
