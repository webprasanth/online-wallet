using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Users;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Queries.Transactions;
using OnlineWallet.Infrastructure.Queries.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controllers.ControllerBase
    {
        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <summary>
        /// Gets a profile of the User
        /// </summary>
        /// <returns></returns>
        // GET: api/User
        [HttpGet("")]
        public async Task<IActionResult> GetUser()
        {
            var user = await DispatchAsync<GetUser, UserDto>(new GetUser());

            return Ok(user);
        }

        [HttpGet("Transactions")]
        public async Task<IActionResult> GetTransactions(GetTransactionsWithDetails query)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactions = await DispatchAsync<GetTransactionsWithDetails, IEnumerable<TransactionDto>>(query);

            return Ok(transactions);
        }

        /// <summary>
        /// Gets data about the User's transactions that can be used to make a charts
        /// </summary>
        /// <returns></returns>
        // GET: api/User/Dashboard
        [HttpGet("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var dashboardData = await DispatchAsync<GetDashboardData, DashboardDataDto>(new GetDashboardData());

            return Ok(dashboardData);
        }


        /// <summary>
        /// Changes phone number of the User
        /// </summary>
        /// <param name="command">Number</param>
        /// <returns></returns>
        // PUT: api/User/UpdatePhone
        [HttpPut("UpdatePhone")]
        public async Task<IActionResult> ChangePhoneNumber([FromBody]ChangePhoneNumber command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await DispatchAsync(command);

            return NoContent();
        }

        /// <summary>
        /// Changes address of the User
        /// </summary>
        /// <param name="command">Address</param>
        /// <returns></returns>
        // PUT: api/User/UpdateAddress
        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> ChangeAddress([FromBody]ChangeAddress command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await DispatchAsync(command);

            return NoContent();
        }

        /// <summary>
        /// Changes password of the User
        /// </summary>
        /// <param name="command">password</param>
        /// <returns></returns>
        // PUT: api/User/UpdatePassword
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePassword command)
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
