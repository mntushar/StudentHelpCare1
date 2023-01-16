namespace StudentHelpCare.Maps
{
    public class TeacherMap
    {
        public TeacherMap() 
        {
        }

        public WebApplication InitialiseTeacherMap(WebApplication app)
        {
            app.MapGet("/teacher", GetAll);

            return app;
        }

        protected async Task<IResult> GetAll()
        {
            return TypedResults.Ok(await Task.Run(() => "Hello Teacher!"));
        }
    }
}
