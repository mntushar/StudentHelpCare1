using StudentHelpCare.Data.Model;
using StudentHelpCare.Services.IServices;

namespace StudentHelpCare.Maps
{
    public static class UserLoginMap
    {
        public static WebApplication InitialiseUserLoginMap(WebApplication app)
        {
            var loginMap = app.MapGroup("/login");

            loginMap.MapPost("/", UserLogin);

            return app;
        }

        private static async Task<IResult> UserLogin(IUserAuthenticationServices authenticationServices, UserLoginModel userLogin)
        {
            return TypedResults.Ok(await authenticationServices.UserLogin(userLogin));
        }
    }
}
