using System.Threading.Tasks;
using OnlineWallet.Core.Domain;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string mail);
        Task RegisterAsync(string email, string password, string fullName);
    }
}