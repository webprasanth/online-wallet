using System;
using System.Threading.Tasks;
using AutoMapper;
using OnlineWallet.Core;
using OnlineWallet.Core.Domain;
using OnlineWallet.Infrastructure.Dto;
using static OnlineWallet.Infrastructure.ErrorCodes;

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
                throw new ServiceException(EmailInUse,"User with such email already exists");
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
                throw new ServiceException(UserNotFound,"User not found");
            }

            if (!user.ValidatePassword(password))
            {
                throw new ServiceException(InvalidCredentials,"Invalid credentials.");
            }
        }

        public async Task ChangePhoneNumberAsync(Guid id, string phoneNumber)
        {
            var user = await _unitOfWork.Users.GetAsync(id);

            if (!int.TryParse(phoneNumber,out int number))
            {
                throw  new ServiceException(InvalidValue,"Phone number must consist of up to 9 digits");
            }

            user.SetPhoneNumber(number);
            _unitOfWork.Save();
        }

        public async Task<UserDto> GetAsync(string mail)
        {
            var user = await _unitOfWork.Users.GetAsync(mail);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);
            return _mapper.Map<UserDto>(user);
        }

    }
}