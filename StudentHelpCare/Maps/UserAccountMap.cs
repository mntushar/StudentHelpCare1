using StudentHelpCare.StudentHelpCare.Data.Model;
using StudentHelpCare.StudentHelpCare.Services.IServices;
using StudentHelpCare.StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.StudentHelpCare.Maps
{
    public static class UserAccountMap
    {
        public static WebApplication InitialiseUserAccountMap(WebApplication app)
        {
            app.MapPost("/Registration", UserRegristration);
            app.MapPost("/login", UserLogin);

            return app;
        }

        private static async Task<IResult> UserRegristration(IUserAccountServices userRegistrationServices, UserViewModel user)
        {
            return TypedResults.Ok(await userRegistrationServices.CreateUser(user));
        }

        private static async Task<IResult> UserLogin(IUserAccountServices userRegistrationServices, UserLoginModel userLogin)
        {
            return TypedResults.Ok(await userRegistrationServices.UserLogin(userLogin));
        }
    }
}
