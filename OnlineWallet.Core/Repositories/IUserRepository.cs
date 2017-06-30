using System;
using System.Threading.Tasks;
using OnlineWallet.Core.Domain;

namespace OnlineWallet.Core.Repositories
{
    public interface IUserRepository
    {

        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);

        Task AddAsync(User user);

        Task RemoveAsync(Guid id);
    }
}