using System;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries.Transactions;

namespace OnlineWallet.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ITransactionQueries _transactionQueries;

        public DashboardService(ITransactionQueries transactionQueries)
        {
            _transactionQueries = transactionQueries;
        }
        public async Task<DashboardDataDto> GetDashboardDataAsync(Guid userId)
        {

            var getAllTransactionsQuery = new GetAllTransactionsWithDetails()
            {
                UserId = userId
            };

            var allTransactions = await _transactionQueries.GetAllTransactionsWithDetailsAsync(getAllTransactionsQuery);

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
                        if (transaction.UserTo == userId.ToString())
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