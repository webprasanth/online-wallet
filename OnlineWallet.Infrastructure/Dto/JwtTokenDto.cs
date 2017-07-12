
namespace OnlineWallet.Infrastructure.Dto
{
    public class JwtTokenDto
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}
