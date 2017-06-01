using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public interface IUserActivityService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactions(Guid userId);
        Task<IEnumerable<DepositDto>> GetAllDeposits(Guid userId);
        Task<IEnumerable<WithdrawalDto>> GetAllWithdrawals(Guid userId);
        Task<IEnumerable<TransferDto>> GetAllTransfers(Guid userId);
    }
}