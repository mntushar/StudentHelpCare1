namespace StudentHelpCare.Library
{
    public static class AppInformation
    {
        private static readonly string appUrl = "";
        private static readonly string appSecretKey = "";

        public static string GetAppUrl()
        {
            return appUrl;
        }

        public static string GetAppSecretKey()
        {
            return appSecretKey;
        }
    }
}
