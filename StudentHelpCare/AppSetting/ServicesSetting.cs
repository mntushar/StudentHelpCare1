using StudentHelpCare.Services.IServices;
using StudentHelpCare.Services.Services;
using System.Runtime.CompilerServices;

namespace StudentHelpCare.AppSetting
{
    public static class ServicesSetting
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IStudentServices, StudentServices>();

            return services;
        }
    }
}
