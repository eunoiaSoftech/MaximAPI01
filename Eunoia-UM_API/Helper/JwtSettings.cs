namespace Eunoia_UM_API.Helper
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double TokenExpiryMinutes { get; set; }

        public int TokenExpiryDays { get; set; }
    }
}
