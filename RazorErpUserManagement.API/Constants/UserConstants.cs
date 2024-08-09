using RazorErpUserManagement.API.Models;

namespace RazorErpUserManagement.API.Constants
{
    public class UserConstants
    {
        public static List<UserDetails> Users = new List<UserDetails>()
        {
            new UserDetails() {Username = "admin", Password = "admin", Email = "admin", Role = "admin", GivenName = "admin", Surname = "admin"},
            new UserDetails() {Username = "user", Password = "user", Email = "user", Role = "user", GivenName = "user", Surname = "user"}
        };
    }
}