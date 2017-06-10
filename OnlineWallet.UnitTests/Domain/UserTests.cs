using System;
using FluentAssertions;
using OnlineWallet.Core.Domain;
using OnlineWallet.Infrastructure.Services;
using Xunit;

namespace OnlineWallet.UnitTests.Domain
{
    public class UserTests
    {
        private readonly User _user;

        public UserTests()
        {
            _user = new User("some@mail.net","some password123","Some Name",123456789,"Salt Test City");
        }

        [Fact]
        public void SetPassword_should_throw_DomainException()
        {
            var exception = Record.Exception(() => _user.SetPassword(""));

            exception.Should().BeOfType<DomainException>();
        }

        [Theory]
        [InlineData("properPassword123")]
        [InlineData("4Hidden123_.,")]
        public void SetPassword_should_change_Password_to_given_value(string password)
        {
            _user.SetPassword(password);

            _user.Password.Should().BeEquivalentTo(password);
        }
    }
}