using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Api
{
    [Route("api/[controller]")]
    public class UserController : Controllers.ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet("")]
        public async Task<UserDto> Get()
        {
            var user = await _userService.GetAsync(UserId);

            return user;
        }

    }
}
