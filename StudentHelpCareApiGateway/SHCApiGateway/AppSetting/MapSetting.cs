using SHCApiGateway.Maps;
using StudentHelpCareIdentityServer.Maps;

namespace StudentHelpCare.StudentHelpCareIdentityServer.AppSetting
{
    public static class MapSetting
    {
        public static WebApplication RegisterMap(this WebApplication app)
        {
            //initialise index map
            IndexMap.InitialiseMap(app);

            //initialise register map
            UserMap.InitialiseMap(app);

            //initialise register map
            UserAuthenticationMap.InitialiseMap(app);

            return app;
        }
    }
}
