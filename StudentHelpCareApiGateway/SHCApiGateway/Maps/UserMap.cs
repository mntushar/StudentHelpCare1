using SHCApiGateway.Services.Iservices;
using SHCApiGateway.ViewModel.User;

namespace SHCApiGateway.Maps
{
    public static class UserMap
    {
        public static WebApplication InitialiseRegisterMap(WebApplication app)
        {
            var registerMap = app.MapGroup("/register");

            registerMap.MapPost("/registerUser", CreateUser);

            return app;
        }

        private static async Task<IResult> CreateUser(IUserServices registerServices, UserViewModel user)
        {
            return TypedResults.Ok(await registerServices.CreateUser(user));
        }
    }
}
