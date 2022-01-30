using MediatR;

namespace GringottsBank.Application.Abstractions
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
