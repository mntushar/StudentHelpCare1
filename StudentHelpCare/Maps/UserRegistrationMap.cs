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

        private static async Task<IResult> UserRegristration(UserViewModel user)
        {
            if(user.Password == user.RepeatPassword)
            {

            }
            return TypedResults.Ok(await Task.Run(() => "test"));
        }
    }
}
