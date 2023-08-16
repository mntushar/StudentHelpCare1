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
            registerMap.MapGet("/refreshToken/", UserRefreshToken);

            return app;
        }

        private static async Task<IResult> UserLogin(IUserAuthentication userAuthentication, UserLoginModel user)
        {
            if (user == null)
            {
                return TypedResults.BadRequest(string.Empty);
            }

            var result = await userAuthentication.UserLogin(user);

            if (!result.Success)
            {
                return TypedResults.NotFound(result);
            }

            return TypedResults.Ok(result);
        }

        private static async Task<IResult> UserRefreshToken(IUserAuthentication userAuthentication, string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return TypedResults.BadRequest(string.Empty);
            }

            var result = await userAuthentication.UserRefreshToken(refreshToken);

            if (!result.Success)
            {
                return TypedResults.NotFound(result);
            }

            return TypedResults.Ok(result);
        }
    }
}
