using SHCApiGateway.Maps;
using StudentHelpCareIdentityServer.Maps;

namespace StudentHelpCare.StudentHelpCareIdentityServer.AppSetting
{
    public static class MapSetting
    {
        public static WebApplication RegisterMap(this WebApplication app)
        {
            //initialise index map
            IndexMap.InitialiseIndexMap(app);

            //initialise register map
            UserMap.InitialiseRegisterMap(app);

            return app;
        }
    }
}
