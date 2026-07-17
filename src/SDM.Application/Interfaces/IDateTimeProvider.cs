namespace SDM.Application.Interfaces;

/// <summary>
/// Abstraction over the system clock.
/// Allows deterministic time-based logic in handlers and testable date/time operations.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>Gets the current UTC date and time.</summary>
    DateTime UtcNow { get; }

    /// <summary>Gets the current local date and time.</summary>
    DateTime Now { get; }

    /// <summary>Gets today's UTC date with the time component set to midnight.</summary>
    DateOnly UtcToday { get; }
}
