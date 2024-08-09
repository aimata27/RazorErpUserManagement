using RazorErpUserManagement.API.Models;

namespace RazorErpUserManagement.API.Interfaces
{
    public interface ILoginService
    {
        UserDetails Authenticate(UserLogin userLogin);
        string GenerateToken(UserDetails userDetails);
    }
}