using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Queries.Transactions;
using OnlineWallet.Infrastructure.Services;

namespace OnlineWallet.Infrastructure.Handlers.Transactions
{
    public class GetDashboardDataHandler : IQueryHandler<GetDashboardData,DashboardDataDto>
    {
        private readonly IDashboardService _dashboardService;

        public GetDashboardDataHandler(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<DashboardDataDto> RetrieveAsync(GetDashboardData query)
        {
            return await _dashboardService.GetDashboardDataAsync(query.UserId);
        }
    }
}