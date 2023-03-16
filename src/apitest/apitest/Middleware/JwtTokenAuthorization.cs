using apitest.Configuration;
using apitest.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace apitest.Middleware
{
    public class JwtTokenAuthorization
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtTokenAuthorization> _logger;
        public JwtTokenAuthorization(RequestDelegate next, ILogger<JwtTokenAuthorization> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachAccountToContext(context, token);

            await _next(context);
        }
        private void attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthenticationOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthenticationOptions.AUDIENCE,
                    IssuerSigningKey = AuthenticationOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromMinutes(AuthenticationOptions.LIFE_TIME)
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                context.Items["User"] = new JwtPerson("", "", Models.Enums.UserType.SuperUser);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to auth");
            }
        }
    }
}
