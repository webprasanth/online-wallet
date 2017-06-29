using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineWallet.Core;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    //public class UserActivityService : IUserActivityService
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMapper _mapper;

    //    public UserActivityService(IUnitOfWork unitOfWork, IMapper mapper)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _mapper = mapper;
    //    }

    //    public async Task<IEnumerable<TransactionDto>> GetAllTransactions(Guid userId)
    //    {
    //        var transfers = await _unitOfWork.Transactions.GetAllTransfersAsync(userId);
    //        var deposits = await _unitOfWork.Transactions.GetAllDepositsAsync(userId);
    //        var withdrawals = await _unitOfWork.Transactions.GetAllWithdrawalsAsync(userId);

    //        var tranasctions = transfers
    //            .Select(t => new TransactionDto {Id = t.Id, Amount = t.Amount, Date = t.Date,Type = "Transfer", UserFrom = t.UserFrom.Email ,UserTo = t.UserTo.Email})
    //            .Union(deposits.Select(
    //                d => new TransactionDto {Id = d.Id, Amount = d.Amount, Date = d.Date, Type = "Deposit", UserFrom = "", UserTo = ""}))
    //            .Union(withdrawals.Select(
    //                w => new TransactionDto {Id = w.Id, Amount = w.Amount, Date = w.Date, Type = "Withdrawal", UserFrom = "", UserTo = "" }));

    //        return tranasctions.OrderByDescending(t => t.Date);
    //    }


    //}
}
