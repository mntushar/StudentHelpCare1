using StudentHelpCare.StudentHelpCare.Repository.IRepository;
using StudentHelpCare.StudentHelpCare.Repository.Repository;

namespace StudentHelpCare.StudentHelpCare.AppSetting
{
    public static class RepositorySetting
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();

            return services;
        }
    }
}
