using RazorErpUserManagement.API.Models.Entities;

namespace RazorErpUserManagement.API.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersByCompany();
    }
}