using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Transactions;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.UI.Framework.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class TransactionsController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public TransactionsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher,queryDispatcher)
        {
        }

        [HttpGet]
        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(Transfer command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Transfer");
            }

            Logger.Info("Processing transfer");

            await DispatchAsync(command);

            Logger.Info("Transfer successful");

            return RedirectToAction("Activity", "Users");

        }

        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(Deposit command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Deposit");
            }

            Logger.Info("Processing deposit");

            await DispatchAsync(command);

            Logger.Info("Deposit successful");

            return RedirectToAction("Activity", "Users");

        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(Withdraw command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Withdraw");
            }
            Logger.Info("Processing withdrawal");

            await DispatchAsync(command);

            Logger.Info("Withdrawal successful");

            return RedirectToAction("Activity", "Users");

        }

    }
}
