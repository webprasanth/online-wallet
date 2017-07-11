using System;
using System.Threading.Tasks;
using Moq;
using OnlineWallet.Core;
using OnlineWallet.Core.Domain;
using OnlineWallet.Infrastructure.Services;
using Xunit;

namespace OnlineWallet.UnitTests.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public TransactionServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task DepositAsync_should_invoke_AddAsync_on_repository()
        {
            var userMock = new Mock<User>();
            _unitOfWorkMock.Setup(x => x.Users.GetAsync(userMock.Object.Id)).Returns(Task.FromResult(userMock.Object));
            _unitOfWorkMock.Setup(x => x.Transactions.AddAsync(It.IsAny<Deposit>())).Returns(Task.CompletedTask);
            var transactionService = new TransactionService(_unitOfWorkMock.Object);

            await transactionService.DepositAsync(1, userMock.Object.Id);

            _unitOfWorkMock.Verify(x => x.Transactions.AddAsync(It.IsAny<Deposit>()),Times.Once);
            _unitOfWorkMock.Verify(x => x.Save(),Times.Once);
        }
    }
}