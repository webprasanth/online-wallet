using Microsoft.EntityFrameworkCore;
using OnlineWallet.Core.Domain;

namespace OnlineWallet.Infrastructure.Data
{
    public class OnlineWalletContext : DbContext
    {
        public OnlineWalletContext()
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

    }
}