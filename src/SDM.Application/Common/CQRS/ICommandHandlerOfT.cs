using MediatR;

namespace SDM.Application.Common.CQRS;

/// <summary>
/// Handler interface for commands that return a typed response.
/// Implement this interface to handle an <see cref="ICommand{TResponse}"/> and return a
/// <see cref="Result{TResponse}"/>.
/// </summary>
/// <typeparam name="TCommand">The command type. Must implement <see cref="ICommand{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">The type of value returned by the command handler.</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
