using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using OnlineWallet.Infrastructure.Dto;

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

        public async Task<IEnumerable<TransactionDto>> GetTransactionsWithDetailsAsync(GetTransactionsWithDetails query)
        {
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlBase = @"SELECT [T].[Id]
                              ,[T].[Amount]
                              ,[T].[Date]
                              ,[T].[Discriminator] as [Type]
                              ,[Users].[Email] as [UserFrom]
                              ,[Users2].[Email] as [UserTo]
                          FROM (([OWDb].[dbo].[Transactions] [T]
						  JOIN [Users] ON [T].[UserFromId] = [Users].[Id])
						  LEFT JOIN [Users] [Users2] ON [T].[UserToId] = [Users2].[Id])
                          WHERE ([UserFromId] = @UserId OR [UserToId] = @UserId) ";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UserId", query.UserId);

                StringBuilder sqlSB = new StringBuilder(sqlBase);

                //encapsulate and add more filters
                if (!string.IsNullOrWhiteSpace(query.Type))
                {
                    sqlSB.Append(" AND [T].[Discriminator] = @type ");
                    parameters.Add("type",query.Type);
                }
                if (!string.IsNullOrWhiteSpace(query.MinAmount))
                {
                    sqlSB.Append(" AND [T].[Amount] > @min ");
                    parameters.Add("min", query.MinAmount);
                }
                if (!string.IsNullOrWhiteSpace(query.MaxAmount))
                {
                    sqlSB.Append(" AND [T].[Amount] < @max ");
                    parameters.Add("max", query.MaxAmount);
                }

                const string sqlOrder = " ORDER BY [T].[Date] DESC;";
                sqlSB.Append(sqlOrder);

                var sql = sqlSB.ToString();
                var transactionDtos = await connection.QueryAsync<TransactionDto>(sql, parameters);

                return transactionDtos;
            }
        }
    }
}