using System.Threading.Tasks;
using OnlineWallet.Infrastructure.Dto;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Queries.Users;

namespace OnlineWallet.Infrastructure.Handlers.Users
{
    public class GetUserHandler : IQueryHandler<GetUser,UserDto>
    {
        private readonly IUserQueries _userQueries;

        public GetUserHandler(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }

        public async Task<UserDto> RetrieveAsync(GetUser query)
        {
            return await _userQueries.GetUserAsync(query.UserId);
        }
    }
}