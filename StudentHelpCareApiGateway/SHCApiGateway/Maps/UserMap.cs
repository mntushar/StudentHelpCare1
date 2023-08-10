using SHCApiGateway.Data.Model;
using SHCApiGateway.Services.Iservices;
using SHCApiGateway.ViewModel.User;

namespace SHCApiGateway.Maps
{
    public static class UserMap
    {
        public static WebApplication InitialiseMap(WebApplication app)
        {
            var registerMap = app.MapGroup("/user");

            registerMap.MapPost("/registerUser", CreateUser);
            registerMap.MapPost("/RoleCreate", CreateRole).RequireAuthorization();

            return app;
        }

        private static async Task<IResult> CreateUser(IUserServices userServices, UserViewModel user)
        {
            if (user == null)
            {
                return TypedResults.BadRequest(string.Empty);
            }

            return TypedResults.Ok(await userServices.CreateUser(user));
        }

        private static async Task<IResult> CreateRole(IUserServices userServices, RoleModel role)
        {
            if (role == null)
            {
                return TypedResults.BadRequest(string.Empty);
            }

            return TypedResults.Ok(await userServices.CreateRole(role));
        }
    }
}
