using Microsoft.IdentityModel.Tokens;
using RazorErpUserManagement.API.Constants;
using RazorErpUserManagement.API.Interfaces;
using RazorErpUserManagement.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RazorErpUserManagement.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;

        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserDetails userDetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDetails.Username),
                new Claim(ClaimTypes.Email, userDetails.Email),
                new Claim(ClaimTypes.GivenName, userDetails.GivenName),
                new Claim(ClaimTypes.Surname, userDetails.Surname),
                new Claim(ClaimTypes.Role, userDetails.Role),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserDetails Authenticate(UserLogin userLogin)
        {
            var user = UserConstants.Users.FirstOrDefault(x => x.Username.Equals(userLogin.Username, StringComparison.CurrentCultureIgnoreCase)
            && x.Password.Equals(userLogin.Password, StringComparison.CurrentCultureIgnoreCase));

            if (user != null)
                return user;

            return null;
        }
    }
}
