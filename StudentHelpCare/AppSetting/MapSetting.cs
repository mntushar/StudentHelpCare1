using StudentHelpCare.Maps;

namespace StudentHelpCare.AppSetting
{
    public static class MapSetting
    {
        public static WebApplication RegisterMap(this WebApplication app)
        {
            //initialise index map
            IndexMap.InitialiseIndexMap(app);

            //user registration
            UserRegistrationMap.InitialiseUserRegistrationMap(app);

            //user login
            UserLoginMap.InitialiseUserLoginMap(app);

            //initialise student map
            StudentMap.InitialiseStudentMap(app);

            //initialise teacher map
            TeacherMap.InitialiseTeacherMap(app);

            return app;
        }
    }
}
