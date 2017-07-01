using System;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public interface IDashboardService
    {
        Task<DashboardDataDto> GetDashboardDataAsync(Guid userId);
    }
}