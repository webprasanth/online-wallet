using System;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Users;
using OnlineWallet.Infrastructure.Services;

namespace OnlineWallet.Infrastructure.Handlers.Users
{
    public class ChangeAddressHandler : ICommandHandler<ChangeAddress>
    {
        private readonly IUserService _userService;

        public ChangeAddressHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ChangeAddress command)
        {
            await _userService.ChangeAddressAsync(command.UserId, command.Address);
        }
    }
}