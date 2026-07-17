namespace SDM.Domain.ValueObjects;

/// <summary>
/// Value Object representing the contact information of a customer on an order.
/// Groups CustomerName, PhoneNumber, and Address as a single, cohesive unit.
/// Equality is based on field values, not identity.
/// </summary>
public sealed class CustomerContact
{
    /// <summary>Gets the full name of the customer.</summary>
    public string CustomerName { get; }

    /// <summary>Gets the customer's phone number.</summary>
    public string PhoneNumber { get; }

    /// <summary>Gets the customer's delivery address.</summary>
    public string Address { get; }

    /// <summary>
    /// Initializes a new <see cref="CustomerContact"/>.
    /// </summary>
    /// <param name="customerName">Full name of the customer. Required.</param>
    /// <param name="phoneNumber">Phone number of the customer. Required.</param>
    /// <param name="address">Delivery address. Required.</param>
    /// <exception cref="ArgumentException">Thrown when any required field is null or whitespace.</exception>
    public CustomerContact(string customerName, string phoneNumber, string address)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name is required.", nameof(customerName));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address is required.", nameof(address));

        CustomerName = customerName.Trim();
        PhoneNumber = phoneNumber.Trim();
        Address = address.Trim();
    }

    /// <summary>
    /// Returns true if both contacts have identical name, phone, and address.
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj is not CustomerContact other) return false;
        return CustomerName == other.CustomerName
            && PhoneNumber == other.PhoneNumber
            && Address == other.Address;
    }

    /// <inheritdoc/>
    public override int GetHashCode() =>
        HashCode.Combine(CustomerName, PhoneNumber, Address);

    /// <inheritdoc/>
    public override string ToString() =>
        $"{CustomerName} | {PhoneNumber} | {Address}";
}
