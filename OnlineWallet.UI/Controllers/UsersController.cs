using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Controllers
{
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserActivityService _userActivityService;

        public UsersController(IUserService userService,IUserActivityService userActivityService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
            _userActivityService = userActivityService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetAsync(UserId);

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Activity()
        {
            var transactions = await _userActivityService.GetAllTransactions(UserId);
            return View(transactions);
        }
    }
}
