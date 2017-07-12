using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Users;
using OnlineWallet.Infrastructure.Extensions;
using OnlineWallet.Infrastructure.Services;

namespace OnlineWallet.Infrastructure.Handlers.Users
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMemoryCache _memoryCache;

        public LoginHandler(IUserService userService,IJwtService jwtService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _jwtService = jwtService;
            _memoryCache = memoryCache;
        }
        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetAsync(command.Email);
            var token = _jwtService.CreateToken(user.Id);

            _memoryCache.SetToken(command.TokenId, token);
        }
    }
}
