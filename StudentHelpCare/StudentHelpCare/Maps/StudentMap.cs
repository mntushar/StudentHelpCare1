using StudentHelpCare.StudentHelpCare.Services.IServices;
using StudentHelpCare.StudentHelpCare.ViewModel.Student;

namespace StudentHelpCare.StudentHelpCare.Maps
{
    public static class StudentMap
    {
        public static WebApplication InitialiseStudentMap(WebApplication app)
        {
            var studentMap = app.MapGroup("/student");

            studentMap.MapGet("/", GetStudent);
            studentMap.MapPost("/create", CreateStudent);

            return app;
        }

        private static async Task<IResult> GetStudent(IStudentServices studentServices)
        {
            return TypedResults.Ok(await studentServices.GetItemListAsync());
        }

        private static async Task<IResult> CreateStudent(IStudentServices studentServices, StudentViewModal student)
        {
            return TypedResults.Ok(await studentServices.InsertItemAsync(student));
        }
    }
}
