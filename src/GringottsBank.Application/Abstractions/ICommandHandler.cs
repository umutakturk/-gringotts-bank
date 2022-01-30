using MediatR;

namespace GringottsBank.Application.Abstractions
{
    public interface ICommandHandler<in TCommand>
        : IRequestHandler<TCommand> where TCommand : ICommand
    {
    }
}
