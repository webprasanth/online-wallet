using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using OnlineWallet.Core;
using OnlineWallet.Core.Domain;
using OnlineWallet.Infrastructure.Services;
using Xunit;

namespace OnlineWallet.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public UserServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
        }


        [Fact]
        public async Task RegisterAsync_should_call_AddAsync_on_repository()
        {
            _unitOfWorkMock.Setup(x => x.Users.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            var userService = new UserService(_unitOfWorkMock.Object, _mapperMock.Object);

            await userService.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            _unitOfWorkMock.Verify(x => x.Users.AddAsync(It.IsAny<User>()),Times.Once);
            _unitOfWorkMock.Verify(x => x.Save(),Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_should_throw_ServiceException_when_user_with_given_email_exists()
        {
            const string email = "some@email.com";
            var user = new User(email,"validPassword","valid fname");
            _unitOfWorkMock.Setup(x => x.Users.GetAsync(email)).Returns(Task.FromResult(user));
            var userService = new UserService(_unitOfWorkMock.Object, _mapperMock.Object);

           var exception = await Record.ExceptionAsync(async () =>
               {
                   await userService.RegisterAsync(email, It.IsAny<string>(), It.IsAny<string>());
               });

            exception.Should().BeOfType<ServiceException>();
        }


    }
}