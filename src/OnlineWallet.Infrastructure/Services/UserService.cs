using System;
using AutoMapper;
using OnlineWallet.Core.Domain;
using OnlineWallet.Core.Repositories;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void Register(string email, string password, string fullName)
        {
            var user = _userRepository.Get(email);

            if (user != null)
            {
                throw new Exception("User with such email already exists");
            }

            user = new User(email,password,fullName);
        }

        public UserDto Get(string mail)
        {
            var user = _userRepository.Get(mail);

            return _mapper.Map<User, UserDto>(user);
        }
    }
}