using MediatR;

namespace SDM.Application.Common.CQRS;

/// <summary>
/// Handler interface for commands that produce no return value.
/// Implement this interface to handle an <see cref="ICommand"/> and return a <see cref="Result"/>.
/// </summary>
/// <typeparam name="TCommand">The command type. Must implement <see cref="ICommand"/>.</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}
