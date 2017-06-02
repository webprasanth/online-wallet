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
                })
            .CreateMapper();


    }
}