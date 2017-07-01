using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Queries.Transactions;
using OnlineWallet.Infrastructure.Queries.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineWallet.UI.Api
{
    [Route("api/[controller]")]
    public class UserController : Controllers.ControllerBase
    {
        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }

        // GET: api/User
        [HttpGet("")]
        public async Task<UserDto> Get()
        {
            var user = await DispatchAsync<GetUser, UserDto>(new GetUser());

            return user;
        }

        [HttpGet("Dashboard")]
        public async Task<DashboardDataDto> Dashboard()
        {
            var dashboardData = await DispatchAsync<GetDashboardData, DashboardDataDto>(new GetDashboardData());

            return dashboardData;
        }
    }
}
