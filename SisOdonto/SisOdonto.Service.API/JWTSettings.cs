namespace SisOdonto.Service.API
{
    public class JWTSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string ValidHost { get; set; }
    }
}
