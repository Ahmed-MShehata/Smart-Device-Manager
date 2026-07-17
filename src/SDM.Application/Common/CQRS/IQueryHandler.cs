using MediatR;

namespace SDM.Application.Common.CQRS;

/// <summary>
/// Handler interface for queries that return a typed response.
/// Implement this interface to handle an <see cref="IQuery{TResponse}"/> and return a
/// <see cref="Result{TResponse}"/>.
/// Implementations must not modify any state.
/// </summary>
/// <typeparam name="TQuery">The query type. Must implement <see cref="IQuery{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">The type of data returned by the query handler.</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
