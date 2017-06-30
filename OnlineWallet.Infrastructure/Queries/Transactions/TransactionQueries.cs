using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using OnlineWallet.Infrastructure.Dto;
using X.PagedList;

namespace OnlineWallet.Infrastructure.Queries.Transactions
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

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsWithDetailsAsync(GetAllTransactionsWithDetails query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"SELECT [T].[Id]
                              ,[T].[Amount]
                              ,[T].[Date]
                              ,[T].[Discriminator] as [Type]
                              ,[Users].[Email] as [UserFrom]
                              ,[Users2].[Email] as [UserTo]
                          FROM (([OWDb].[dbo].[Transactions] [T]
						  JOIN [Users] ON [T].[UserFromId] = [Users].[Id])
						  LEFT JOIN [Users] [Users2] ON [T].[UserToId] = [Users2].[Id])
                          WHERE [UserFromId] = @UserId OR [UserToId] = @UserId
						  ORDER BY [T].[Date] DESC;";

                var transactionDtos = await connection.QueryAsync<TransactionDto>(sql, new { UserId = query.UserId});

                return transactionDtos;
            }
        }

        public async Task<IPagedList<TransactionDto>> GetTransactionsWithDetailsAsync(GetTransactionsWithDetails query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"SELECT [T].[Id]
                              ,[T].[Amount]
                              ,[T].[Date]
                              ,[T].[Discriminator] as [Type]
                              ,[Users].[Email] as [UserFrom]
                              ,[Users2].[Email] as [UserTo]
                          FROM (([OWDb].[dbo].[Transactions] [T]
						  JOIN [Users] ON [T].[UserFromId] = [Users].[Id])
						  LEFT JOIN [Users] [Users2] ON [T].[UserToId] = [Users2].[Id])
                          WHERE [UserFromId] = @UserId OR [UserToId] = @UserId
						  ORDER BY [T].[Date] DESC;";

                var transactionDtos = await connection.QueryAsync<TransactionDto>(sql, new { UserId = query.UserId });

                var pagedTransactions = await transactionDtos.ToPagedListAsync(query.Page, query.ItemsPerPage);

                return pagedTransactions;
            }
        }
    }
}