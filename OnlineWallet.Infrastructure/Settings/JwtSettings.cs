namespace OnlineWallet.Infrastructure.Settings
{
    public class JwtSettings
    {
        public JwtSettings()
        {
            
        }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string ExpiryMinutes { get; set; }
    }
}