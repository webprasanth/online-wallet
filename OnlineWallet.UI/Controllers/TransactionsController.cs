﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Transactions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Controllers
{
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public TransactionsController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
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

            command.UserId = UserId;
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

            command.UserId = UserId;
            await DispatchAsync(command);

            Logger.Info("Deposit successful");

            return RedirectToAction("Index", "Users");

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

            command.UserId = UserId;
            await DispatchAsync(command);

            Logger.Info("Withdrawal successful");

            return RedirectToAction("Index", "Users");

        }

    }
}
