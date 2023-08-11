using SHCApiGateway.Data.Entity;
using SHCApiGateway.Library;
using SHCApiGateway.Services.Iservices;
using SHCApiGateway.Services.Services;

namespace StudentHelpCare.StudentHelpCare.AppSetting
{
    public static class ServicesSetting
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IUserAuthentication, UserAuthentication>();
            services.AddTransient<ICryptography<User>, Cryptography<User>>();

            return services;
        }
    }
}
