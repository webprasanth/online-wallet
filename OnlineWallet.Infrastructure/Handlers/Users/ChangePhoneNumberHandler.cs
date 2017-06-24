using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Users;
using OnlineWallet.Infrastructure.Services;

namespace OnlineWallet.Infrastructure.Handlers.Users
{
    public class ChangePhoneNumberHandler : ICommandHandler<ChangePhoneNumber>
    {
        private readonly IUserService _userService;

        public ChangePhoneNumberHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(ChangePhoneNumber command)
        {
            await _userService.ChangePhoneNumberAsync(command.UserId, command.Number);
        }
    }
}