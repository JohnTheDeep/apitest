using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace apitest.Configuration
{
    public class AuthenticationOptions
    {
        public const string ISSUER = "https://localhost:7201";
        public const string AUDIENCE = "http://localhost:5183";
        public const string KEY = "qwdf13sfasff1213f";
        public const int LIFE_TIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
