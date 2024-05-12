namespace UniFood.Utils
{
    public static class ConfigUtil
    {
        public static string ConnectionString { get; set; }
        public static string ApiKey { get; set; }
        public static string JWTAudience { get; set; }
        public static string JWTIssuer { get; set; }
        public static string JWTKey { get; set; }
    }
}
