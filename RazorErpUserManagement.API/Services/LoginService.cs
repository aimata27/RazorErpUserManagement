using Dapper;
using Microsoft.IdentityModel.Tokens;
using RazorErpUserManagement.API.Interfaces;
using RazorErpUserManagement.API.Models.Data;
using RazorErpUserManagement.API.Models.Dto;
using RazorErpUserManagement.API.Models.Entities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RazorErpUserManagement.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly DapperDbContext _dbContext;
        private readonly IDbConnection _dbConnection;

        public LoginService(IConfiguration configuration, DapperDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _dbConnection = _dbContext.CreateConnection();
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> Authenticate(UserLogin userLogin)
        {
            string sqlQuery = "SELECT * FROM Users WHERE " +
                $"Username = '{userLogin.Username}' AND Password = '{userLogin.Password}'";

            var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(sqlQuery);

            if (user != null)
                return user;

            return null;
        }
    }
}
