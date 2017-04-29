﻿using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}