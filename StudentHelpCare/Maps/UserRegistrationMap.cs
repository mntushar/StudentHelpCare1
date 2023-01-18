using StudentHelpCare.Services.IServices;
using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Maps
{
    public static class UserRegistrationMap
    {
        public static WebApplication InitialiseUserRegistrationMap(WebApplication app)
        {
            var studentMap = app.MapGroup("/Registration");

            studentMap.MapPost("/", UserRegristration);

            return app;
        }

        private static async Task<IResult> UserRegristration(IUserRegistrationServices userRegistrationServices, UserViewModel user)
        {
            return TypedResults.Ok(await userRegistrationServices.CreateUser(user));
        }
    }
}
