using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Users;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Controllers
{
    [Authorize]
    public class UsersController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IUserService _userService;
       // private readonly IUserActivityService _userActivityService;
        private readonly ITransactionQueries _transactionQueries;

        [ActionContext]
        public ActionContext ActionContext { get; set; }

        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher, ITransactionQueries transactionQueries) : base(commandDispatcher)
        {
            _userService = userService;
            //_userActivityService = userActivityService;
            _transactionQueries = transactionQueries;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            Logger.Info("Fetching User' profile");

            var user = await _userService.GetAsync(UserId);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Activity()
        {
            Logger.Info("Fetching User' activity");

            //var transactions = await _userActivityService.GetAllTransactions(UserId);
            var transactions = await _transactionQueries.GetTransactionsWithDetailsAsync(UserId);
            return View(transactions);
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhoneNumber(ChangePhoneNumber command)
        {
            ActionContext.RouteData.Values["action"] = "Edit";

            Logger.Info("Changing phone number");

            await DispatchAsync(command);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAddress(ChangeAddress command)
        {
            ActionContext.RouteData.Values["action"] = "Edit";

            Logger.Info("Changing Address");

            await DispatchAsync(command);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword command)
        {
            ActionContext.RouteData.Values["action"] = "Edit";

            Logger.Info("Changing Password");

            await DispatchAsync(command);

            return RedirectToAction("Index");
        }
    }
}
