using StudentHelpCareIdentity.Services.Iservices;
using StudentHelpCareIdentity.Services.Services;

namespace StudentHelpCare.StudentHelpCare.AppSetting
{
    public static class ServicesSetting
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IRegisterUserServices, RegisterUserServices>();

            return services;
        }
    }
}
