using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public interface IUserActivityService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactions(Guid userId);
    }
}