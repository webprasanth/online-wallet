﻿using System;
using System.Threading.Tasks;
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

        public async Task RegisterAsync(string email, string password, string fullName)
        {
            var user = await _userRepository.GetAsync(email);

            if (user != null)
            {
                throw new Exception("User with such email already exists");
            }

            user = new User(email,password,fullName);

            await _userRepository.AddAsync(user);
        }

        public async Task<UserDto> GetAsync(string mail)
        {
            var user = await _userRepository.GetAsync(mail);

            return _mapper.Map<User, UserDto>(user);
        }
    }
}