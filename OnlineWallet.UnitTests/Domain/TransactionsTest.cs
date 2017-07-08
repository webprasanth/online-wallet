using System;
using FluentAssertions;
using Moq;
using OnlineWallet.Core.Domain;
using Xunit;

namespace OnlineWallet.UnitTests.Domain
{
    public class TransactionsTest
    {
        private static readonly User[] Users;
        static TransactionsTest()
        {
            Users = new[]
            {
                new User("test@test.com", "password123", "Full Name"),
                new User("test2@test2.com", "2password2", "Full2 Name2")
            };
        }

        [Theory]
        [InlineData(5.58)]
        [InlineData(1)]
        [InlineData(123)]
        [InlineData(10000000)]
        public void Deposit_should_be_created_for_given_values(decimal value)
        {
            var deposit = new Deposit(value, Users[0]);

            deposit.UserFrom.Id.Should().Be(Users[0].Id);
            deposit.Amount.Should().Be(value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.99)]
        [InlineData(-5.20)]
        [InlineData(-8)]
        [InlineData(10000000.01)]
        [InlineData(10000001.00)]
        public void Deposit_should_throw_DomainException_for_given_values(decimal value)
        {

            var deposit = new Deposit(value, Users[0]);

            deposit.UserFrom.Id.Should().Be(Users[0].Id);
            deposit.Amount.Should().Be(value);
        }

        [Theory]
        [InlineData(5.58)]
        [InlineData(1)]
        [InlineData(123)]
        [InlineData(10000000)]
        public void Withdrawal_should_be_created_for_given_values(decimal value)
        {
            var withdrawal = new Withdrawal(value, Users[0]);

            withdrawal.UserFrom.Id.Should().Be(Users[0].Id);
            withdrawal.Amount.Should().Be(value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.99)]
        [InlineData(-5.20)]
        [InlineData(-8)]
        [InlineData(10000000.01)]
        [InlineData(10000001.00)]
        public void Withdrawal_should_throw_DomainException_for_given_values(decimal value)
        {

            var withdrawal = new Withdrawal(value, Users[0]);

            withdrawal.UserFrom.Id.Should().Be(Users[0].Id);
            withdrawal.Amount.Should().Be(value);
        }

        [Theory]
        [InlineData(5.58)]
        [InlineData(1)]
        [InlineData(123)]
        [InlineData(10000000)]
        public void Transfer_should_be_created_for_given_values(decimal value)
        {

            var transfer = new Transfer(value, Users[0],Users[1]);

            transfer.UserFrom.Id.Should().Be(Users[0].Id);
            transfer.UserTo.Id.Should().Be(Users[1].Id);
            transfer.Amount.Should().Be(value);
        }

        [Fact]
        public void Transfer_should_throw_DomainException_for_the_same_user()
        {
            var value = 100;
            var exception = Record.Exception(() => new Transfer(value, Users[0], Users[0]));

            exception.Should().BeOfType<DomainException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.99)]
        [InlineData(-5.20)]
        [InlineData(-8)]
        [InlineData(10000000.01)]
        [InlineData(10000001.00)]
        public void Transfer_should_throw_DomainException_for_given_values(decimal value)
        {

            var transfer = new Transfer(value, Users[0], Users[1]);

            transfer.UserFrom.Id.Should().Be(Users[0].Id);
            transfer.UserTo.Id.Should().Be(Users[1].Id);
            transfer.Amount.Should().Be(value);
        }
    }
}