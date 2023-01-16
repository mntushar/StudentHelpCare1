using StudentHelpCare.Maps;

namespace StudentHelpCare.AppSetting
{
    public static class MapSetting
    {
        public static WebApplication RegisterMap(this WebApplication app)
        {
            //initialise student map
            StudentMap studentMap = new StudentMap();
            studentMap.InitialiseStudentMap(app);

            //initialise teacher map
            TeacherMap teacherMap = new TeacherMap();
            teacherMap.InitialiseTeacherMap(app);

            return app;
        }
    }
}
