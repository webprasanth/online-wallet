using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NLog;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Services;
using OnlineWallet.UI.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AccountController(IUserService userService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register");
            }
            Logger.Info("Processing registration");

            await _userService.RegisterAsync(viewModel.Email, viewModel.Password, viewModel.FullName);

            Logger.Info("Registration successful");

            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            Logger.Info("Processing logging in");

            await _userService.LoginAsync(viewModel.Email, viewModel.Password);
            var user = await _userService.GetAsync(viewModel.Email);
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString("N")),
                    new Claim(ClaimTypes.Name, user.Id.ToString("N")),
                    new Claim(ClaimTypes.Email, user.Email)
                };
            var identity = new ClaimsIdentity(claims, "password");
            await HttpContext.Authentication.SignInAsync("Cookie", new ClaimsPrincipal(identity));

            Logger.Info("Logging in successful");

            return RedirectToAction("Index", "Users");

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            Logger.Info("Processing logging out");

            await HttpContext.Authentication.SignOutAsync("Cookie");

            Logger.Info("Logging out successful");

            return RedirectToAction("Index", "Home");
        }

    }

}
