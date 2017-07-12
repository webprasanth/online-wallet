using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public interface IJwtService
    {
        JwtTokenDto CreateToken(string email);
    }
}