using MediatR;

namespace SDM.Application.Common.CQRS;

/// <summary>
/// Marker interface for commands that return a typed response.
/// Implement this interface to define a write operation that produces a result value.
/// Used with <see cref="ICommandHandler{TCommand, TResponse}"/>.
/// </summary>
/// <typeparam name="TResponse">The type of value returned by the command.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
