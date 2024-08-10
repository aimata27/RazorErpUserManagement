using RazorErpUserManagement.API.Models.Dto;
using RazorErpUserManagement.API.Models.Entities;

namespace RazorErpUserManagement.API.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetAllUsersAsync();
        void CreateUserAsync(UserDetails user);
        void UpdateUserAsync(UserDetails userDetails, int userId);
        void DeleteUserAsync(int userId);
        Task<bool> IsCompanyExistsAsync(int companyId);
    }
}