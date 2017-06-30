using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Queries.Users
{
    public class UserQueries : IUserQueries
    {
        private readonly string _connectionString;

        public UserQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));

        }
        public async Task<UserDto> GetUserAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"SELECT [Id]
                              ,[Address]
                              ,[CreatedAt]
                              ,[Email]
                              ,[FullName]
                              ,[Password]
                              ,[PhoneNumber]
                              ,[Balance]
                          FROM [OWDb].[dbo].[Users]
                          WHERE [Id] = @Id;";

                var userDto = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { Id = id });

                return userDto;
            }
        }

        public async Task<UserDto> GetUserAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"SELECT [Id]
                              ,[Address]
                              ,[CreatedAt]
                              ,[Email]
                              ,[FullName]
                              ,[Password]
                              ,[PhoneNumber]
                              ,[Balance]
                          FROM [OWDb].[dbo].[Users]
                          WHERE [Email] = @Email;";

                var userDto = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { Email = email });

                return userDto;
            }
        }
    }
}