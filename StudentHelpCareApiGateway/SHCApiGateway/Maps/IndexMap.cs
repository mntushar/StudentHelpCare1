namespace StudentHelpCareIdentityServer.Maps
{
    public static class IndexMap
    {
        public static WebApplication InitialiseMap(WebApplication app)
        {
            app.MapGet("/", () => Get());

            return app;
        }

        private static async Task<IResult> Get()
        {
            return TypedResults.Ok(await Task.Run(() => "Hello Identity Server!"));
        }
    }
}
