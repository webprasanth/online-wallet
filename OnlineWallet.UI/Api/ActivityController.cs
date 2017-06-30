using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Api
{
    [Route("api/[controller]")]
    public class ActivityController : Controllers.ControllerBase
    {

        public ActivityController(ICommandDispatcher commandDispatcher,IQueryDispatcher queryDispatcher) : base(commandDispatcher,queryDispatcher)
        {
        }
        // GET: api/Activity
        [HttpGet("")]
        public async Task<IEnumerable<TransactionDto>> GetAll(GetTransactionsWithDetails query)
        {
            var transactions = await DispatchAsync<GetTransactionsWithDetails, IEnumerable<TransactionDto>>(query);

            return transactions;
        }
    }
}
