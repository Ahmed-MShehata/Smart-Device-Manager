using SDM.Application.Interfaces;

namespace SDM.Infrastructure.Services;

/// <summary>
/// Production implementation of <see cref="IDateTimeProvider"/>.
/// Delegates to <see cref="DateTime"/> and <see cref="DateOnly"/> system APIs.
/// </summary>
internal sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc/>
    public DateTime UtcNow => DateTime.UtcNow;

    /// <inheritdoc/>
    public DateTime Now => DateTime.Now;

    /// <inheritdoc/>
    public DateOnly UtcToday => DateOnly.FromDateTime(DateTime.UtcNow);
}
