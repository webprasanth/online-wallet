using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Users;
using OnlineWallet.Infrastructure.Extensions;
using OnlineWallet.Infrastructure.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Api
{
    [Route("api/Account")]
    public class AccountApiController : Controllers.ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AccountApiController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IMemoryCache memoryCache) : base(commandDispatcher, queryDispatcher)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Logs in the User
        /// </summary>
        /// <param name="command"></param>
        /// <returns>bearer token</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]Login command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Logger.Info("Processing logging in");

            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);

            var jwtToken = _memoryCache.GetToken(command.TokenId);

           return Ok(jwtToken.Token);
        }
    }
}
