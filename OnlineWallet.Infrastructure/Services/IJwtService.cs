using System;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public interface IJwtService
    {
        JwtTokenDto CreateToken(Guid userId);
    }
}