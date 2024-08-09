using RazorErpUserManagement.API.Interfaces;
using RazorErpUserManagement.API.Services;

namespace RazorErpUserManagement.API
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<ILoginService, LoginService>();

            return services;
        }
    }
}
