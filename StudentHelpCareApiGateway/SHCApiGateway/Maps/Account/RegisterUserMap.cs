using StudentHelpCareIdentity.Services.Iservices;
using StudentHelpCareIdentity.ViewModel.User;

namespace StudentHelpCareIdentity.Maps.Account
{
    public static class RegisterUserMap
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
