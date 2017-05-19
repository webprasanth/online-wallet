using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;

namespace OnlineWallet.UI.Controllers
{
    [Authorize]
    public abstract class ControllerBase : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        protected Guid UserId
            => HttpContext.User.Identity.IsAuthenticated ? Guid.Parse(HttpContext.User.Identity.Name) : Guid.Empty;

        protected ControllerBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command is IAuthenticatedCommand authenticatedCommand)
            {
                authenticatedCommand.UserId = UserId;
            }

            await _commandDispatcher.DispatchAsync(command);
        }

    }
}
