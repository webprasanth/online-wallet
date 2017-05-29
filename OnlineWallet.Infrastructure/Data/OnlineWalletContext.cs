using Microsoft.EntityFrameworkCore;
using OnlineWallet.Core.Domain;

namespace OnlineWallet.Infrastructure.Data
{
    public class OnlineWalletContext : DbContext
    {
        public OnlineWalletContext(DbContextOptions<OnlineWalletContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User

            modelBuilder.Entity<User>()
                .Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Email);

            modelBuilder.Entity<User>()
                .Property(u => u.Password).IsRequired().HasMaxLength(64);

            modelBuilder.Entity<User>()
                .Property(u => u.FullName).IsRequired();

            #endregion

            #region Transaction

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount).IsRequired().IsConcurrencyToken();

            modelBuilder.Entity<Deposit>();
            modelBuilder.Entity<Withdrawal>();
            modelBuilder.Entity<Transfer>();
            #endregion

            modelBuilder.Entity<Account>();
        }

    }
}