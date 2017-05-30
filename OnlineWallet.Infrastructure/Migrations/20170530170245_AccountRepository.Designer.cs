using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OnlineWallet.Infrastructure.Data;

namespace OnlineWallet.Infrastructure.Migrations
{
    [DbContext(typeof(OnlineWalletContext))]
    [Migration("20170530170245_AccountRepository")]
    partial class AccountRepository
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineWallet.Core.Domain.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("OnlineWallet.Core.Domain.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid?>("UserFromId");

                    b.HasKey("Id");

                    b.HasIndex("UserFromId");

                    b.ToTable("Transactions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Transaction");
                });

            modelBuilder.Entity("OnlineWallet.Core.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccountId");

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int?>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.HasIndex("AccountId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineWallet.Core.Domain.Deposit", b =>
                {
                    b.HasBaseType("OnlineWallet.Core.Domain.Transaction");


                    b.ToTable("Deposit");

                    b.HasDiscriminator().HasValue("Deposit");
                });

            modelBuilder.Entity("OnlineWallet.Core.Domain.Transfer", b =>
                {
                    b.HasBaseType("OnlineWallet.Core.Domain.Transaction");

                    b.Property<Guid?>("UserToId");

                    b.HasIndex("UserToId");

                    b.ToTable("Transfer");

                    b.HasDiscriminator().HasValue("Transfer");
                });

            modelBuilder.Entity("OnlineWallet.Core.Domain.Withdrawal", b =>
                {
                    b.HasBaseType("OnlineWallet.Core.Domain.Transaction");


                    b.ToTable("Withdrawal");

                    b.HasDiscriminator().HasValue("Withdrawal");
                });

            modelBuilder.Entity("OnlineWallet.Core.Domain.Transaction", b =>
                {
                    b.HasOne("OnlineWallet.Core.Domain.User", "UserFrom")
                        .WithMany()
                        .HasForeignKey("UserFromId");
                });

            modelBuilder.Entity("OnlineWallet.Core.Domain.User", b =>
                {
                    b.HasOne("OnlineWallet.Core.Domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("OnlineWallet.Core.Domain.Transfer", b =>
                {
                    b.HasOne("OnlineWallet.Core.Domain.User", "UserTo")
                        .WithMany()
                        .HasForeignKey("UserToId");
                });
        }
    }
}
