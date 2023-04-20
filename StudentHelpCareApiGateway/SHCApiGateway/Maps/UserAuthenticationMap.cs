using SHCApiGateway.Services.Iservices;
using StudentHelpCare.Identity.Data.Model;

namespace SHCApiGateway.Maps
{
    public static class UserAuthenticationMap
    {
        public static WebApplication InitialiseMap(WebApplication app)
        {
            var registerMap = app.MapGroup("/authentication");

            registerMap.MapPost("/login", UserLogin);

            return app;
        }

        private static async Task<IResult> UserLogin(IUserAuthentication userAuthentication, UserLoginModel user)
        {
            if (user == null)
            {
                return TypedResults.BadRequest(string.Empty);
            }
            
            return TypedResults.Ok(await userAuthentication.UserLogin(user));
        }
    }
}
