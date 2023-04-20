using Newtonsoft.Json.Linq;
using StudentHelpCare.Identity.Data.Model;

namespace SHCApiGateway.Services.Iservices
{
    public interface IUserAuthentication
    {
        Task<JObject> UserLogin(UserLoginModel userLogin);
    }
}
