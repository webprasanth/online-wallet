using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Transactions;
using OnlineWallet.Infrastructure.Services;

namespace OnlineWallet.Infrastructure.Handlers.Transactions
{
   public class DepositHandler : ICommandHandler<Deposit>
    {
        private readonly ITransactionService _transactionService;

        public DepositHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public async Task HandleAsync(Deposit command)
        {
            await _transactionService.DepositAsync(command.Amount, command.UserId);
        }
    }
}
