namespace StudentHelpCare.Maps
{
    public class StudentMap
    {
        public StudentMap()
        {
        }

        public WebApplication InitialiseStudentMap(WebApplication app)
        {
            app.MapGet("/", GetAll);

            return app;
        }

        protected async Task<IResult> GetAll()
        {
            return TypedResults.Ok(await Task.Run(() => "Hello Student!"));
        }
    }
}
