using SDM.Application.Common.CQRS;

namespace SDM.Application.SystemComponents.GetSystemComponent;

/// <summary>
/// Query to retrieve full details of a specific system component by its ID.
/// </summary>
public sealed class GetSystemComponentQuery : IQuery<GetSystemComponentResponse>
{
    /// <summary>Gets the unique identifier of the component to retrieve.</summary>
    public Guid Id { get; init; }
}
