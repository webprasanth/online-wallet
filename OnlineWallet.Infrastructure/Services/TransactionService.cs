using System;
using System.Threading.Tasks;
using OnlineWallet.Core;
using OnlineWallet.Core.Domain;
using static OnlineWallet.Infrastructure.ErrorCodes;


namespace OnlineWallet.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task DepositAsync(decimal amount,Guid userId)
        {
            if (amount <= 0)
            {
                throw new ServiceException(InvalidValue,"amount of deposit must be greater than 0.");
            }

            var userMakingDeposit = await _unitOfWork.Users.GetAsync(userId);
            userMakingDeposit.IncreaseBalance(amount);

            var deposit = new Deposit(amount, userMakingDeposit);
            await _unitOfWork.Transactions.AddAsync(deposit);
            _unitOfWork.Save();
        }

        public async Task WithdrawAsync(decimal amount, Guid userId)
        {
            if (amount <= 0)
            {
                throw new ServiceException(InvalidValue, "amount of withdrawal must be greater than 0.");
            }

            var userWithdrawing = await _unitOfWork.Users.GetAsync(userId);

            userWithdrawing.ReduceBalance(amount);

            var withdrawal = new Withdrawal(amount, userWithdrawing);
            await _unitOfWork.Transactions.AddAsync(withdrawal);
            _unitOfWork.Save();
        }

        public async Task TransferAsync(decimal amount, Guid userId, string mailTo)
        {
            if (amount <= 0)
            {
                throw new ServiceException(InvalidValue, "amount of transfer must be greater than 0.");
            }

            var userReceivingTransfer = await _unitOfWork.Users.GetAsync(mailTo);

            if (userReceivingTransfer == null)
            {
                throw new ServiceException(UserNotFound, "user of given email does not exists.");
            }

            var userMakingTransfer = await _unitOfWork.Users.GetAsync(userId);

            if (userReceivingTransfer.Id == userMakingTransfer.Id)
            {
                throw new ServiceException(InvalidValue, "You cannot transfer money to yourself");
            }

            userMakingTransfer.ReduceBalance(amount);
            userReceivingTransfer.IncreaseBalance(amount);

            var transfer = new Transfer(amount,userMakingTransfer,userReceivingTransfer);
            await _unitOfWork.Transactions.AddAsync(transfer);
            _unitOfWork.Save();
        }
    }
}