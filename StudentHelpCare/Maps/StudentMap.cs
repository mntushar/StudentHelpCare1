using StudentHelpCare.Services.IServices;
using StudentHelpCare.ViewModel.Student;

namespace StudentHelpCare.Maps
{
    public class StudentMap
    {
        public WebApplication InitialiseStudentMap(WebApplication app)
        {
            app.MapGet("/student", (IStudentServices studentService) => GetStudent(studentService));
            app.MapPost("/student/create", (IStudentServices studentServices, StudentViewModal student) => Create(studentServices, student));

            return app;
        }

        protected async Task<IResult> GetStudent(IStudentServices studentServices)
        {
            return TypedResults.Ok(await studentServices.GetItemListAsync());
        }

        protected async Task<IResult> Create(IStudentServices studentServices, StudentViewModal student)
        {
            return TypedResults.Ok(await studentServices.InsertItemAsync(student));
        }
    }
}
