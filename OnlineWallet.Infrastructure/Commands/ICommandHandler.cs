using System.Threading.Tasks;

namespace OnlineWallet.Infrastructure.Commands
{
    public interface ICommandHandler<T>
    {
        Task HandleAsync(T command);
    }
}