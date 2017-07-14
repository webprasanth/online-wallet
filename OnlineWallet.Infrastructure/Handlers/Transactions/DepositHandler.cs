using System.Globalization;
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
            decimal amount;
            if (!decimal.TryParse(command.Amount, NumberStyles.Any, new CultureInfo("en-US"), out amount))
            {
                throw new ServiceException(ErrorCodes.InvalidValue, "Invalid format. Use dot instead of comma");
            }

            await _transactionService.DepositAsync(amount, command.UserId);
        }
    }
}
