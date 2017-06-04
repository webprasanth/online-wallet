using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Commands;
using OnlineWallet.Infrastructure.Commands.Transactions;
using OnlineWallet.Infrastructure.Services;

namespace OnlineWallet.Infrastructure.Handlers.Transactions
{
    public class WihdrawHandler : ICommandHandler<Withdraw>
    {
        private readonly ITransactionService _transactionService;

        public WihdrawHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public async Task HandleAsync(Withdraw command)
        {
            decimal amount;
            decimal.TryParse(command.Amount.Replace('.', ','), out amount);

            await _transactionService.WithdrawAsync(amount, command.UserId);
        }
    }
}
