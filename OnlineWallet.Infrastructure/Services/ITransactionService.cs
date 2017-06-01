using System;
using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.Services
{
    public interface ITransactionService
    {
        Task DepositAsync(decimal amount, Guid userId);

        Task WithdrawAsync(decimal amount, Guid userId);

        Task TransferAsync(decimal amount, Guid userId, string mailTo); 
    }
}