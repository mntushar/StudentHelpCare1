namespace StudentHelpCare.Maps
{
    public static class TeacherMap
    {
        public static WebApplication InitialiseTeacherMap(WebApplication app)
        {
            app.MapGet("/teacher", GetAll);

            return app;
        }

        private static async Task<IResult> GetAll()
        {
            return TypedResults.Ok(await Task.Run(() => "Hello Teacher!"));
        }
    }
}
