using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.User;
using OnlineWallet.Infrastructure.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommandDispatcher _commandDispatcher;

        public UserController(IUserService userService, ICommandDispatcher commandDispatcher)
        {
            _userService = userService;
            _commandDispatcher = commandDispatcher;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetAsync("user1@aol.com");

            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUser command,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                await _commandDispatcher.DispatchAsync(command);

                if (string.IsNullOrWhiteSpace(returnUrl))
                   return RedirectToAction("Index", "User");
                else
                    return Redirect(returnUrl);
            }

            return View();
        }
    }
}
