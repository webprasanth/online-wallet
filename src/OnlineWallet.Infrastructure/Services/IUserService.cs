using OnlineWallet.Core.Domain;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public interface IUserService
    {
        UserDto Get(string mail);
        void Register(string email, string password, string fullName, int phoneNumber, string address);
    }
}