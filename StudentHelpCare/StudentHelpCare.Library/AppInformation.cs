namespace StudentHelpCare.StudentHelpCare.Library
{
    public static class AppInformation
    {
        private static readonly string appUrl = "https://localhost:7110/";

        public static string GetAppUrl()
        {
            return appUrl;
        }
    }
}
