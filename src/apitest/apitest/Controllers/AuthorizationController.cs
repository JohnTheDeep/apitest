using apitest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthOps = apitest.Configuration.AuthenticationOptions;
using SignInCred = Microsoft.IdentityModel.Tokens.SigningCredentials;
namespace apitest.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly List<JwtPerson> _jwtPeople = new List<JwtPerson>
        {
            new JwtPerson("admin","root",Models.Enums.UserType.DefaultUser),
            new JwtPerson("superuser@mail.ru","testpassword",Models.Enums.UserType.SuperUser)
        };

        [AllowAnonymous]
        [HttpPost("/Authorization")]
        public async Task<IActionResult> Authorization(string login, string password)
        {
            var identity = await GetIdentity(login, password);
            if (identity is null)
                return BadRequest(new { error = "Bad request" });
            var utcNow = DateTime.UtcNow;
            var s = AuthOps.GetSymmetricSecurityKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AuthOps.ISSUER,
                Audience = AuthOps.AUDIENCE,
                Subject = identity,
                Expires = utcNow.Add(TimeSpan.FromMinutes(AuthOps.LIFE_TIME)),
                SigningCredentials = new SignInCred(AuthOps.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return Json(new
            {
                access_token = stringToken,
                username = identity.Name
            });
        }
        [AllowAnonymous]
        [HttpPost("/GetIdentity")]
        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            JwtPerson person = _jwtPeople
                .FirstOrDefault(el => el.Login == login && el.Password == password);
            if (person != null)
            {
                List<Claim> claims = new()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString())
                };
                return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            }
            return null;
        }
    }
}
