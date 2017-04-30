using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using FluentAssertions;
using Moq;
using OnlineWallet.Core.Domain;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Services;

namespace OnlineWallet.UnitTests.Services
{
     public class UserServiceTests
     {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task RegisterAsync_should_invoke_once_AddAsync_into_repository()
        {
            var userService = new UserService(_userRepositoryMock.Object,_mapperMock.Object);
            await userService.RegisterAsync("user@test.com", "Testing Password", "Testing User");

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()),Times.Once);
        }

         [Fact]
         public async Task GetASync_should_invoke_once_repository_GetAsync()
         {
             var userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
             await userService.GetAsync("user@test.com");

             var user = new User("user@test.com", "Testing Password", "Testing User");
             _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(user);

            _userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()),Times.Once);

         }
    }
}
