using System;
using Microsoft.Extensions.Caching.Memory;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Extensions
{
    public static class MemoryCacheExtensions
    {
        public static void SetToken(this IMemoryCache memoryCache, Guid tokenId, JwtTokenDto jwtToken)
        {
            memoryCache.Set<JwtTokenDto>(tokenId, jwtToken);
        }

        public static JwtTokenDto GetToken(this IMemoryCache memoryCache, Guid tokenId)
        {
            return memoryCache.Get<JwtTokenDto>(tokenId);
        }
    }
}