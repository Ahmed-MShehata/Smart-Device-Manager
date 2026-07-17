namespace SDM.Domain.ValueObjects;

/// <summary>
/// Value Object representing a reference to the customer's device.
/// Wraps the device unique identifier string with domain semantics.
/// Equality is based on the DeviceId value, not identity.
/// </summary>
public sealed class DeviceReference
{
    /// <summary>Gets the unique identifier of the customer's device.</summary>
    public string DeviceId { get; }

    /// <summary>
    /// Initializes a new <see cref="DeviceReference"/>.
    /// </summary>
    /// <param name="deviceId">The unique device identifier. Required and non-empty.</param>
    /// <exception cref="ArgumentException">Thrown when deviceId is null or whitespace.</exception>
    public DeviceReference(string deviceId)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
            throw new ArgumentException("Device ID is required.", nameof(deviceId));

        DeviceId = deviceId.Trim();
    }

    /// <summary>
    /// Returns true if both references point to the same device.
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj is not DeviceReference other) return false;
        return DeviceId == other.DeviceId;
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(DeviceId);

    /// <inheritdoc/>
    public override string ToString() => DeviceId;
}
