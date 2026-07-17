using MediatR;

namespace SDM.Application.Common.CQRS;

/// <summary>
/// Marker interface for queries that return a typed response.
/// Implement this interface to define a read-only operation that returns data.
/// Queries must not modify any state.
/// Used with <see cref="IQueryHandler{TQuery, TResponse}"/>.
/// </summary>
/// <typeparam name="TResponse">The type of data returned by the query.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
