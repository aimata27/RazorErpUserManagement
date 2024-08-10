using RazorErpUserManagement.API.Interfaces;
using RazorErpUserManagement.API.Models.Data;
using RazorErpUserManagement.API.Services;

namespace RazorErpUserManagement.API
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<ILoginService, LoginService>()
                .AddTransient<DapperDbContext>()
                .AddScoped<IAdminService, AdminService>()
                .AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
