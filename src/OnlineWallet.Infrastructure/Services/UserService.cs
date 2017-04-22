using System;
using OnlineWallet.Core.Domain;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(string email, string password, string fullName, int phoneNumber, string address)
        {
            var user = _userRepository.Get(email);

            if (user != null)
            {
                throw new Exception("User with such email already exists");
            }

            user = new User(email,password,fullName,phoneNumber,address);
        }

        public UserDto Get(string mail)
        {
            var user = _userRepository.Get(mail);

            return new UserDto()
            {
                Account = user.Account,
                Address = user.Address,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FullName = user.FullName,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}