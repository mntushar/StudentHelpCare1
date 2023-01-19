using StudentHelpCare.Repository;
using StudentHelpCare.Repository.IRepository;
using StudentHelpCare.Repository.Repository;

namespace StudentHelpCare.AppSetting
{
    public static class RepositorySetting
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();

            return services;
        }
    }
}
