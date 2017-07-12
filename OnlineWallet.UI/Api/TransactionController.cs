using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Transactions;
using OnlineWallet.Infrastructure.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Api
{
    [Authorize]
    [EnableCors("default")]
    [Route("api/[controller]")]
    public class TransactionController : Controllers.ControllerBase
    {
        public TransactionController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <summary>
        /// Transfers the money to other User
        /// </summary>
        /// <param name="command">amount</param>
        /// <returns></returns>
        //POST: /api/Transaction/Transfer
        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer([FromBody]Transfer command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await DispatchAsync(command);

            return NoContent();
        }

        /// <summary>
        /// Deposits the money from the User
        /// </summary>
        /// <param name="command">amount</param>
        /// <returns></returns>
        //POST: /api/Transaction/Deposit
        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit([FromBody]Deposit command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await DispatchAsync(command);

            return NoContent();
        }

        /// <summary>
        /// Withdraws the money from the User
        /// </summary>
        /// <param name="command">amount</param>
        /// <returns></returns>
        //POST: /api/Transaction/Withdraw
        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw([FromBody]Withdraw command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await DispatchAsync(command);

            return NoContent();
        }

    }
}
