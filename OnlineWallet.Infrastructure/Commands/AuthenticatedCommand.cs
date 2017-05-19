using System;

namespace OnlineWallet.Infrastructure.Commands
{
    public class AuthenticatedCommand : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
    }
}