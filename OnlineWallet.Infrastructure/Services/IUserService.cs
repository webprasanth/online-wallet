using System;
using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string mail);
        Task<UserDto> GetAsync(Guid id);
        Task RegisterAsync(string email, string password, string fullName);
        Task LoginAsync(string email, string password);
        Task ChangePhoneNumberAsync(Guid id, string phoneNumber);
        Task ChangeAddressAsync(Guid id, string address);
        Task ChangePasswordAsync(Guid id, string currentPassword, string newPassword);
    }
}