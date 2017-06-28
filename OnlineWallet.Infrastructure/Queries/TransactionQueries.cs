using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Queries
{
    public class TransactionQueries : ITransactionQueries
    {
        private readonly string _connectionString;

        public TransactionQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<TransactionDto> GetTransactionAsync(Guid id)
        {
            throw new NotImplementedException(); //TO DO
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync(Guid userId)
        {
            throw new NotImplementedException(); //TO DO
        }
    }
}