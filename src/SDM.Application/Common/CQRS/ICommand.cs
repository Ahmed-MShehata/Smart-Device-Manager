using MediatR;

namespace SDM.Application.Common.CQRS;

/// <summary>
/// Marker interface for commands that produce no return value.
/// Implement this interface to define a write operation that does not return data.
/// Used with <see cref="ICommandHandler{TCommand}"/>.
/// </summary>
public interface ICommand : IRequest<Result>
{
}
