using SHCApiGateway.Data.Entity;
using SHCApiGateway.Services.Iservices;
using SHCApiGateway.ViewModel.User;
using SHCApiGateway.ViewModel.UserRole;

namespace SHCApiGateway.Maps
{
    public static class UserMap
    {
        public static WebApplication InitialiseRegisterMap(WebApplication app)
        {
            var registerMap = app.MapGroup("/user");

            registerMap.MapPost("/registerUser", CreateUser);
            registerMap.MapPost("/RoleCreate", CreateRole);

            return app;
        }

        private static async Task<IResult> CreateUser(IUserServices registerServices, UserViewModel user)
        {
            if (user == null)
            {
                return TypedResults.BadRequest(string.Empty);
            }

            string result = await registerServices.CreateUser(user);

            if (result != "true")
            {
                return TypedResults.BadRequest(result);
            }

            return TypedResults.Created(result);
        }

        private static async Task<IResult> CreateRole(IUserServices registerServices, RoleViewModel role)
        {
            if(role == null)
            {
                return TypedResults.BadRequest(string.Empty);
            }

            string result = await registerServices.CreateRole(role);

            if(result != "true")
            {
                return TypedResults.BadRequest(result);
            }

            return TypedResults.Created(result);
        }
    }
}
