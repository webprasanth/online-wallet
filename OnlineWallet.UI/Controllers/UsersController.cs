using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Users;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries.Transactions;
using OnlineWallet.Infrastructure.Queries.Users;
using X.PagedList;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Controllers
{
    [Authorize]
    public class UsersController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [ActionContext]
        public ActionContext ActionContext { get; set; }

        public UsersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher,queryDispatcher)
        {
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            Logger.Info("Fetching User' profile");

            var user = await DispatchAsync<GetUser, UserDto>(new GetUser());

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Activity(GetTransactionsWithDetails query)
        {
            Logger.Info("Fetching User' activity");

            ViewData["Type"] = query.Type;
            ViewData["Min"] = query.Min;
            ViewData["Max"] = query.Max;

            if (!ModelState.IsValid)
            {
                ViewData.Clear();
                query = new GetTransactionsWithDetails();
            }

            var pagedTransactions = await DispatchAsync<GetTransactionsWithDetails, IPagedList<TransactionDto>>(query);

            return View(pagedTransactions);
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
