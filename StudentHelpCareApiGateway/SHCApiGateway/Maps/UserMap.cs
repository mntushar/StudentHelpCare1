using SHCApiGateway.Data.Model;
using SHCApiGateway.Services.Iservices;
using SHCApiGateway.ViewModel.User;
using SHCApiGateway.ViewModel.UserRole;

namespace SHCApiGateway.Maps
{
    public static class UserMap
    {
        public static WebApplication InitialiseMap(WebApplication app)
        {
            var registerMap = app.MapGroup("/user");

            registerMap.MapPost("/registerUser", CreateUser);
            registerMap.MapPost("/RoleCreate", CreateRole);

            return app;
        }

        private static async Task<IResult> CreateUser(IUserServices userServices, UserViewModel user)
        {
            if (user == null)
            {
                return TypedResults.BadRequest(string.Empty);
            }

            string result = await userServices.CreateUser(user);

            if (result != "true")
            {
                return TypedResults.BadRequest(result);
            }

            return TypedResults.Ok(result);
        }

        private static async Task<IResult> CreateRole(IUserServices userServices, RoleModel role)
        {
            if (role == null)
            {
                return TypedResults.BadRequest(string.Empty);
            }

            string result = await userServices.CreateRole(role);

            if (result != "true")
            {
                return TypedResults.BadRequest(result);
            }

            return TypedResults.Ok(result);
        }
    }
}
