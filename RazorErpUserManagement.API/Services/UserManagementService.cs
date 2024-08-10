using Dapper;
using RazorErpUserManagement.API.Interfaces;
using RazorErpUserManagement.API.Models;
using RazorErpUserManagement.API.Models.Data;
using RazorErpUserManagement.API.Models.Entities;
using System.Data;

namespace RazorErpUserManagement.API.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly DapperDbContext _dbContext;
        private readonly IDbConnection _dbConnection;

        public UserManagementService(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbConnection = _dbContext.CreateConnection();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            string sqlQuery = "SELECT * FROM Users";

            var users = await _dbConnection.QueryAsync<User>(sqlQuery);

            return users.ToList();
        }

        public async void CreateUserAsync(UserDetails user)
        {
            string sqlQuery = "INSERT INTO Users (Username, [Password], Email, [Role], GivenName, Surname, CompanyId) VALUES " +
                $"('{user.Username}', '{user.Password}', '{user.Email}', '{user.Role}', '{user.GivenName}', '{user.Surname}', {user.CompanyId})";

            await _dbConnection.ExecuteAsync(sqlQuery);
        }

        public async void UpdateUserAsync(UserDetails userDetails, int userId)
        {
            string sqlQuery = "UPDATE Users SET " +
                $"Username = '{userDetails.Username}', [Password] = '{userDetails.Password}', " +
                $"Email = '{userDetails.Email}', [Role] = '{userDetails.Role}', " +
                $"GivenName = '{userDetails.GivenName}', Surname = '{userDetails.Surname}', " +
                $"CompanyId = {userDetails.CompanyId} WHERE Id = {userId}";

            await _dbConnection.ExecuteAsync(sqlQuery);
        }

        public async void DeleteUserAsync(int userId)
        {
            string sqlQuery = $"DELETE FROM Users WHERE Id = {userId}";

            await _dbConnection.ExecuteAsync(sqlQuery);
        }

        public async Task<bool> IsCompanyExistsAsync(int companyId)
        {
            string sqlQuery = $"SELECT Company FROM Companies WHERE Id = {companyId}";

            var company = await _dbConnection.QueryFirstOrDefaultAsync<string>(sqlQuery);

            if (company != null)
                return true;

            return false;
        }
    }
}
