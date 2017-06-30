using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineWallet.Core.Domain;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Data;

namespace OnlineWallet.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        public UserRepository(OnlineWalletContext context)
        {
            Context = context;
        }

        public async Task<User> GetAsync(Guid id)
            => await Context.Users.SingleOrDefaultAsync(x => x.Id == id);


        public async Task<User> GetAsync(string email)
        {
            return await Context.Users.SingleOrDefaultAsync(x => x.Email.ToLowerInvariant() == email);
        }

        public async Task AddAsync(User user)
        {
            await Context.Users.AddAsync(user);
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await Context.Users.SingleOrDefaultAsync(t => t.Id == id);
            Context.Users.Remove(user);
        }

        public OnlineWalletContext Context { get; }

    }
}