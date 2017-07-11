using System;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries.Transactions;
using OnlineWallet.Infrastructure.Queries.Users;
using static OnlineWallet.Infrastructure.ErrorCodes;

namespace OnlineWallet.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ITransactionQueries _transactionQueries;
        private readonly IUserQueries _userQueries;

        public DashboardService(ITransactionQueries transactionQueries, IUserQueries userQueries)
        {
            _transactionQueries = transactionQueries;
            _userQueries = userQueries;
        }
        public async Task<DashboardDataDto> GetDashboardDataAsync(Guid userId)
        {

            var allTransactions = await _transactionQueries.GetAllTransactionsWithDetailsAsync(userId);
            var user = await _userQueries.GetUserAsync(userId); // not sufficient. Can be omitted and use just one query

            if (user == null)
            {
                throw new ServiceException(UserNotFound);
            }

            var userEmail = user.Email;
            var withdrawals = 0;
            var deposits = 0;
            var transfers = 0;
            decimal incomes = 0;
            decimal outcomes = 0;

            foreach (var transaction in allTransactions)
            {
                switch (transaction.Type)
                {
                    case "Deposit":
                        deposits++;
                        incomes += transaction.Amount;
                        break;
                    case "Withdrawal":
                        withdrawals++;
                        outcomes += transaction.Amount;
                        break;
                    case "Transfer":
                        transfers++;
                        if (transaction.UserTo == userEmail)
                        {
                            incomes += transaction.Amount;
                        }
                        else
                        {
                            outcomes += transaction.Amount;
                        }
                        break;
                    default:
                        throw new ServiceException("Invalid transaction type found.");
                }
            }

            var dashboardData = new DashboardDataDto()
            {
                DepositsCount = deposits,
                WithdrawalsCount = withdrawals,
                TransfersCount = transfers,
                Incomes = incomes,
                Outcomes = outcomes
            };

            return dashboardData;
        }
    }
}