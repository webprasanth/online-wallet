using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Settings;

namespace OnlineWallet.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _subJwtOptions;

        public JwtService(IOptions<JwtSettings> subJwtOptions)
        {
            _subJwtOptions = subJwtOptions.Value;
        }

        public JwtTokenDto CreateToken(Guid userId)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.Ticks.ToString(), ClaimValueTypes.Integer64)
            };

            var minutes = int.Parse(_subJwtOptions.ExpiryMinutes);

            var expires = now.AddMinutes(minutes);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_subJwtOptions.Key)),
                SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: _subJwtOptions.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtTokenDto()
            {
                Token = token,
                Expires = expires.Ticks
            };
        }
    }
}
