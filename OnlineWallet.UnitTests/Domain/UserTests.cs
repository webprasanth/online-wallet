using FluentAssertions;
using OnlineWallet.Core.Domain;
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

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData("12345")]
        [InlineData("123456789012345678901234567890123")] // > 32 chars
        [InlineData("some password123")]
        public void SetPassword_should_throw_DomainException(string password)
        {
            var exception = Record.Exception(() => _user.SetPassword(""));

            exception.Should().BeOfType<DomainException>();
        }

        [Theory]
        [InlineData("123456")]
        [InlineData("properPassword123")]
        [InlineData("4Hidden123_.,")]
        [InlineData("some password123")]
        public void SetPassword_should_change_Password_to_given_value(string password)
        {
            _user.SetPassword(password);

            _user.Password.Should().BeEquivalentTo(password);
        }

        [Theory]
        [InlineData("test@test.com")]
        [InlineData("test@test.com.pl")]
        [InlineData("123test123@321test.co.uk")]
        [InlineData("1@1.de")]
        [InlineData("some@mail.net")]
        public void SetEmail_should_change_Email_to_given_value(string email)
        {
            _user.SetEmail(email);

            _user.Email.Should().BeEquivalentTo(email);
        }

        [Theory]
        [InlineData("testtest.com")]
        [InlineData("@test.com.pl")]
        [InlineData("test@test")]
        [InlineData("test@1.")]
        [InlineData("@")]
        [InlineData("@.com")]
        [InlineData(".@..")]
        public void SetEmail_should_throw_DomainException(string email)
        {
            var exception = Record.Exception(() => _user.SetEmail(email));

            exception.Should().BeOfType<DomainException>();
        }
    }
}