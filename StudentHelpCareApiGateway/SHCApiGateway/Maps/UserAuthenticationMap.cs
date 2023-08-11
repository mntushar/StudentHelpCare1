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
            registerMap.MapGet("/refreshToken/{refreshToken}", async (IUserAuthentication userAuthentication, string token) =>
            {
                await UserRefreshToken(userAuthentication, token);
            });

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

        private static async Task<IResult> UserRefreshToken(IUserAuthentication userAuthentication, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return TypedResults.BadRequest(string.Empty);
            }

            return TypedResults.Ok(await userAuthentication.UserRefreshToken(token));
        }
    }
}
