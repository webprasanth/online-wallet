using System.Threading.Tasks;
using OnlineWallet.Core.Domain;

namespace OnlineWallet.Infrastructure.Services
{
    public interface ITransactionService
    {
        Task DepositAsync(decimal amount, User user); // TO DO: 2nd arg - user's id

        Task WithdrawAsync(decimal amount, User user); // as above

        Task TransferAsync(decimal amount, User userFrom, User userTo); // TO DO: last arg - user's mail
    }
}