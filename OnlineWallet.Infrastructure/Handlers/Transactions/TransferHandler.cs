using System.Globalization;
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
            if (!decimal.TryParse(command.Amount, NumberStyles.Any, new CultureInfo("en-US"), out amount))
            {
                throw new ServiceException(ErrorCodes.InvalidValue, "Invalid format. Use dot instead of comma");
            }


            await _transactionService.TransferAsync(amount, command.UserId,command.EmailTo);
        }
    }
}
