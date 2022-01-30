using MediatR;

namespace GringottsBank.Application.Abstractions
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
