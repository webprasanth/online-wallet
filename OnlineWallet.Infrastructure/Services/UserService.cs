using System;
using System.Threading.Tasks;
using AutoMapper;
using OnlineWallet.Core;
using OnlineWallet.Core.Domain;
using OnlineWallet.Infrastructure.Dto;

namespace OnlineWallet.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterAsync(string email, string password, string fullName)
        {
            var user = await _unitOfWork.Users.GetAsync(email);
            
            if (user != null)
            {
                throw new InvalidOperationException("User with such email already exists");
            }

            user = new User(email,password,fullName);

            await _unitOfWork.Users.AddAsync(user);
            _unitOfWork.Save();
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _unitOfWork.Users.GetAsync(email);

            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            if (!user.ValidatePassword(password))
            {
                throw new InvalidOperationException("Invalid credentials.");
            }
        }

        public async Task<UserDto> GetAsync(string mail)
        {
            var user = await _unitOfWork.Users.GetAsync(mail);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);

            return _mapper.Map<User, UserDto>(user);
        }

    }
}