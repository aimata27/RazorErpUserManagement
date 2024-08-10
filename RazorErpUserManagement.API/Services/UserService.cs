using Dapper;
using RazorErpUserManagement.API.Interfaces;
using RazorErpUserManagement.API.Models.Data;
using RazorErpUserManagement.API.Models.Entities;
using System.Data;
using System.Security.Claims;

namespace RazorErpUserManagement.API.Services
{
    public class UserService : IUserService
    {
        private readonly DapperDbContext _dbContext;
        private readonly IDbConnection _dbConnection;
        private readonly IHttpContextAccessor _http;

        public UserService(DapperDbContext dbContext, IHttpContextAccessor http)
        {
            _dbContext = dbContext;
            _dbConnection = _dbContext.CreateConnection();
            _http = http;
        }

        public async Task<List<User>> GetAllUsersByCompany()
        {
            Claim claims = _http.HttpContext.User.Claims.FirstOrDefault();

            string companyId = await GetCompanyIdByUser(claims.Value);
            string sqlQuery = $"SELECT * FROM Users WHERE CompanyId = {companyId}";

            var users = await _dbConnection.QueryAsync<User>(sqlQuery);

            return users.ToList();
        }

        private async Task<string> GetCompanyIdByUser(string userId)
        {
            string sqlQuery = $"SELECT CompanyId FROM Users WHERE Id = '{userId}'";
            string companyId = await _dbConnection.QueryFirstOrDefaultAsync<string>(sqlQuery);

            return companyId;
        }
    }
}
