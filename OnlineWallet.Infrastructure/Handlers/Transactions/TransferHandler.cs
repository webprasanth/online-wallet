using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Transactions;
using OnlineWallet.Infrastructure.Services;

namespace OnlineWallet.Infrastructure.Handlers.Transactions
{
    public class TransferHandler : ICommandHandler<Transfer>
    {
        private readonly ITransactionService _transactionService;

        public TransferHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task HandleAsync(Transfer command)
        {
            decimal amount;
            decimal.TryParse(command.Amount.Replace('.', ','), out amount);

            await _transactionService.TransferAsync(amount, command.UserId,command.EmailTo);
        }
    }
}
