using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Maps
{
    public static class UserLoginMap
    {
        public static WebApplication InitialiseUserLoginMap(WebApplication app)
        {
            var loginMap = app.MapGroup("/login");

            loginMap.MapPost("", UserLogin);

            return app;
        }

        private static async Task<IResult> UserLogin()
        {
            return TypedResults.Ok(await Task.Run(() => "Login"));
        }
    }
}
