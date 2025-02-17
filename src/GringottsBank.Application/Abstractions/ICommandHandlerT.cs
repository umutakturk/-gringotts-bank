﻿using MediatR;

namespace GringottsBank.Application.Abstractions
{
    public interface ICommandHandler<in TCommand, TResult>
        : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
    }
}
