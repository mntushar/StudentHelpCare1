namespace StudentHelpCareIdentityServer.Maps
{
    public static class IndexMap
    {
        public static WebApplication InitialiseMap(WebApplication app)
        {
            app.MapGet("/index", () => Get());
            app.MapGet("/index/home", () => Home()).RequireAuthorization();

            return app;
        }

        private static async Task<IResult> Get()
        {
            return TypedResults.Ok(await Task.Run(() => "Hello"));
        }

        private static async Task<IResult> Home()
        {
            return TypedResults.Ok(await Task.Run(() => "Home"));
        }
    }
}
