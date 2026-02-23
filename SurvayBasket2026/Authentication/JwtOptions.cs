namespace SurvayBasket2026.Authentication
{
    public class JwtOptions
    {
        public static string SectionName = "Jwt";

        public string key { get; set; } = string.Empty;
        public string issuer { get; set; } = string.Empty;
        public string audience { get; set; } = string.Empty;
        public int expiryMinutes { get; set; } 
        
    }
}
