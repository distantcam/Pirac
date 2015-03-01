using System.Threading.Tasks;

namespace Pirac
{
    public interface IAsyncCommand : IAsyncCommand<object>
    {
    }

    public interface IAsyncCommand<in T> : IRaiseCanExecuteChanged
    {
        Task ExecuteAsync(T obj);

        bool CanExecute(T obj);
    }
}