﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Services;
using OnlineWallet.UI.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Api
{
    [Route("api/[controller]")]
    public class AccountController : Controllers.ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AccountController(IUserService userService, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IJwtService jwtService) : base(commandDispatcher, queryDispatcher)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("Register");
        //    }
        //    Logger.Info("Processing registration");

        //    await _userService.RegisterAsync(viewModel.Email, viewModel.Password, viewModel.FullName);

        //    Logger.Info("Registration successful");

        //    return RedirectToAction("Login");
        //}
        [AllowAnonymous]
        [HttpGet("Token")]
        public IActionResult Token()
        {
            var token = _jwtService.CreateToken("email");

            return Json(token);
        }

        [Authorize]
        [HttpGet("auth")]
        public IActionResult Auth()
        {

            return Ok("auth");
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

        //[Authorize]
        //public async Task<IActionResult> Logout()
        //{
        //    Logger.Info("Processing logging out");

        //    await HttpContext.Authentication.SignOutAsync("Cookie");

        //    Logger.Info("Logging out successful");

        //    return RedirectToAction("Index", "Home");
        //}
    }
}
