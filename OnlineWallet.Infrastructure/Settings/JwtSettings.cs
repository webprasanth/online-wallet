namespace OnlineWallet.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; } = "1(YouCantSeeMe)2mytimeisnow";
        public int ExpiryMinutes { get; set; } = 10;
        public string Issuer { get; set; } = "http://localhost5000";
    }
}