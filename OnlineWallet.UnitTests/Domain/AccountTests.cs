using OnlineWallet.Core.Domain;
using Xunit;
using FluentAssertions;

namespace OnlineWallet.UnitTests.Domain
{
    public class AccountTests
    {
        private readonly User _user;

        public AccountTests()
        {
            _user = new User("mail@mail.com","password","Full Name");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(15.50)]
        [InlineData(213)]
        public void Balance_should_be_equal_to_(decimal value)
        {
            _user.IncreaseBalance(value);

            _user.Balance.ShouldBeEquivalentTo(value);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5.50)]
        [InlineData(-213)]
        public void IncreaseBalance_should_throw_DomainException_with_given_non_positive_values(decimal value)
        {

            var exception = Record.Exception(() => _user.IncreaseBalance(value));

            exception.Should().BeOfType<DomainException>();
        }


        [Theory]
        [InlineData(10)]
        [InlineData(15.50)]
        [InlineData(213)]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-32)]
        [InlineData(-4.4)]
        public void ReduceBalance_should_throw_DomainException_with_given_exceeded_and_non_positive_values(decimal value)
        {
            _user.IncreaseBalance(5);

            var exception = Record.Exception(() => _user.ReduceBalance(value));

            exception.Should().BeOfType<DomainException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0.01)]
        [InlineData(-6)]
        [InlineData(-23.55)]
        public void IncreaseBalance_Should_throw_DomainException_while_given_negative_value(decimal value)
        {
            var exception = Record.Exception(() => _user.IncreaseBalance(value));

            exception.Should().BeOfType<DomainException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(150)]
        [InlineData(1000000000)]
        [InlineData(52.55)]
        public void IncreaseBalance_should_increase_Balance_by_given_value(decimal value)
        {
            var oldBalance = _user.Balance;
            var newBalance = oldBalance + value;

            _user.IncreaseBalance(value);

            _user.Balance.Should().Be(newBalance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0.01)]
        [InlineData(-6)]
        [InlineData(-23.55)]
        [InlineData(-100.55)]
        [InlineData(100.56)]
        [InlineData(101)]
        public void ReduceBalance_Should_throw_DomainException_while_given_negative_or_greater_value(decimal value)
        {
            _user.IncreaseBalance(100.55M);

            var exception = Record.Exception(() => _user.ReduceBalance(value));

            exception.Should().BeOfType<DomainException>();
        }
    }
}