using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Api
{
    [Route("api/[controller]")]
    public class ActivityController : Controllers.ControllerBase
    {
        private readonly IUserActivityService _userActivityService;

        public ActivityController(IUserActivityService userActivityService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userActivityService = userActivityService;
        }
        // GET: api/Activity
        [HttpGet("")]
        public async Task<IEnumerable<TransactionDto>> GetAll()
        {
            var transactions = await _userActivityService.GetAllTransactions(UserId);

            return transactions;
        }
    }
}
