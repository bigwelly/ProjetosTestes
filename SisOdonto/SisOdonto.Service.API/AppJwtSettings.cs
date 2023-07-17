namespace SisOdonto.Service.API
{
    public class AppJwtSettings
    {
        [Obsolete("For better security use IJwtBuilder and set null for this field")]
        public string SecretKey { get; set; }

        public int Expiration { get; set; } = 1;


        public int RefreshTokenExpiration { get; set; } = 30;


        public string Issuer { get; set; } = "SisOdonto";


        public string Audience { get; set; } = "https://localhost";


        public RefreshTokenType RefreshTokenType { get; set; } = RefreshTokenType.OneTime;

    }
}
