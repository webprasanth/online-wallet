using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Services;
using OnlineWallet.UI.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        //private readonly ICommandDispatcher _commandDispatcher;

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

            try
            {
                await _userService.RegisterAsync(viewModel.Email, viewModel.Password, viewModel.FullName);
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index","Home");
            }

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

            try
            {
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

                return RedirectToAction("Index","Users");
            }
            catch (Exception e)
            {

                return RedirectToAction("Login");
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");
            return RedirectToAction("Index", "Home");
        }

    }

}
