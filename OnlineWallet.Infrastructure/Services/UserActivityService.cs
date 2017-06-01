using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineWallet.Core;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserActivityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactions(Guid userId)
        {
            var transactions = await _unitOfWork.Transactions.GetAllAsync(userId);
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<IEnumerable<DepositDto>> GetAllDeposits(Guid userId)
        {
            var deposits = await _unitOfWork.Transactions.GetAllAsync(userId);
            return _mapper.Map<IEnumerable<DepositDto>>(deposits);
        }

        public async Task<IEnumerable<WithdrawalDto>> GetAllWithdrawals(Guid userId)
        {
            var withdrawals = await _unitOfWork.Transactions.GetAllAsync(userId);
            return _mapper.Map<IEnumerable<WithdrawalDto>>(withdrawals);
        }

        public async Task<IEnumerable<TransferDto>> GetAllTransfers(Guid userId)
        {
            var transfers = await _unitOfWork.Transactions.GetAllAsync(userId);
            return _mapper.Map<IEnumerable<TransferDto>>(transfers);
        }
    }
}
