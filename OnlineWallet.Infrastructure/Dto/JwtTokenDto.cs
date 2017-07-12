
using System;

namespace OnlineWallet.Infrastructure.Dto
{
    public class JwtTokenDto
    {
        public string Token { get; set; }
        public long Expires { get; set; }
        public Guid UserId { get; set; }
    }
}
