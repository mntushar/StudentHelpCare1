namespace StudentHelpCare.Maps
{
    public class IndexMap
    {
        public WebApplication InitialiseIndexMap(WebApplication app)
        {
            app.MapGet("/", () => Get());

            return app;
        }

        protected async Task<IResult> Get()
        {
            return TypedResults.Ok(await Task.Run(() => "Hello World!"));
        }
    }
}
