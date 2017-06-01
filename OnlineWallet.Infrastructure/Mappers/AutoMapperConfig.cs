using AutoMapper;
using OnlineWallet.Core.Domain;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDto>();
                    cfg.CreateMap<Transaction, TransactionDto>();
                    cfg.CreateMap<Transaction, DepositDto>();
                    cfg.CreateMap<Transaction, WithdrawalDto>();
                    cfg.CreateMap<Transaction, TransferDto>();
                })
            .CreateMapper();


    }
}