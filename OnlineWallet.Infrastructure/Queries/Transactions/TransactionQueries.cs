using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"SELECT [Id]
                              ,[Amount]
                              ,[Date]
                              ,[Discriminator] as [Type]
                              ,[UserFromId] as [UserFrom]
                              ,[UserToId] as [UserTo]
                          FROM [OWDb].[dbo].[Transactions]
                          WHERE [Id] = @Id;";

                var transactionDto = await connection.QuerySingleOrDefaultAsync<TransactionDto>(sql, new {Id = id} );

                return transactionDto;
            }
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsWithDetailsAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"SELECT [Transactions].[Id]
                              ,[Transactions].[Amount]
                              ,[Transactions].[Date]
                              ,[Transactions].[Discriminator] as [Type]
                              ,[Users].[Email] as [UserFrom]
                              ,[Users2].[Email] as [UserTo]
                          FROM (([OWDb].[dbo].[Transactions]
						  JOIN [Users] ON [Transactions].[UserFromId] = [Users].[Id])
						  LEFT JOIN [Users] [Users2] ON [Transactions].[UserToId] = [Users2].[Id])
                          WHERE [UserFromId] = @UserId OR [UserToId] = @UserId
						  ORDER BY [Transactions].[Date] DESC;";

                var transactionDtos = await connection.QueryAsync<TransactionDto>(sql, new { UserId = userId.ToString() });

                return transactionDtos;
            }
        }
    }
}