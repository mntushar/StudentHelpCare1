using StudentHelpCare.Services.IServices;
using StudentHelpCare.Services.Services;

namespace StudentHelpCare.AppSetting
{
    public static class ServicesSetting
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenServices, TokenServices>();
            services.AddTransient<IUserAccountServices, UserAccountServices>();
            services.AddTransient<IStudentServices, StudentServices>();

            return services;
        }
    }
}
