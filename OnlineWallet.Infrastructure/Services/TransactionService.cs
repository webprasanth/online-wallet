using System;
using System.Threading.Tasks;
using OnlineWallet.Core.Domain;
using OnlineWallet.Core.Repositories;

namespace OnlineWallet.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;

        public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }
        public async Task DepositAsync(decimal amount,User user)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount of deposit must be greater than 0.");
            }

            var userMakingDeposit = await _userRepository.GetAsync(user.Id);
            userMakingDeposit.Account.IncreaseBalance(amount);

            var deposit = new Deposit(amount, userMakingDeposit);
            await _transactionRepository.AddAsync(deposit);
        }

        public async Task WithdrawAsync(decimal amount, User user)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount of withdrawal must be greater than 0.");
            }

            var userWithdrawing = await _userRepository.GetAsync(user.Id);

            userWithdrawing.Account.ReduceBalance(amount);

            var withdrawal = new Withdrawal(amount, userWithdrawing);
            await _transactionRepository.AddAsync(withdrawal);
        }

        public async Task TransferAsync(decimal amount, User userFrom, User userTo)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount of transfer must be greater than 0.");
            }

            var userMakingTransfer = await _userRepository.GetAsync(userFrom.Id);
            var userReceivingTransfer = await _userRepository.GetAsync(userTo.Id);

            userMakingTransfer.Account.ReduceBalance(amount);
            userReceivingTransfer.Account.IncreaseBalance(amount);

            var transfer = new Transfer(amount,userMakingTransfer,userReceivingTransfer);
            await _transactionRepository.AddAsync(transfer);
        }
    }
}