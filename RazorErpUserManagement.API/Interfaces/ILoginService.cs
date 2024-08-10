using RazorErpUserManagement.API.Models.Dto;
using RazorErpUserManagement.API.Models.Entities;

namespace RazorErpUserManagement.API.Interfaces
{
    public interface ILoginService
    {
        Task<User> Authenticate(UserLogin userLogin);
        string GenerateToken(User userDetails);
    }
}