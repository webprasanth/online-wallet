using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Queries;

namespace OnlineWallet.UI.Controllers
{
    [Authorize]
    public abstract class ControllerBase : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        protected virtual Guid UserId
            => HttpContext.User.Identity.IsAuthenticated ? Guid.Parse(HttpContext.User.Identity.Name) : Guid.Empty;

        protected ControllerBase(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command is IAuthenticatedCommand authenticatedCommand)
            {
                authenticatedCommand.UserId = UserId;
            }

            await _commandDispatcher.DispatchAsync(command);
        }

        protected async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            query.UserId = UserId;

            return await _queryDispatcher.DispatchAsync<TQuery,TResult>(query);
        }

    }
}
