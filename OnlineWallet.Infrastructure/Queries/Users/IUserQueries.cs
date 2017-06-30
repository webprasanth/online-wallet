using System;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Queries.Users
{
    public interface IUserQueries
    {
        Task<UserDto> GetUserAsync(Guid id);
        Task<UserDto> GetUserAsync(string email);
    }
}