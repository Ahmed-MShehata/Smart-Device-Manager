namespace SDM.Domain.Enums;

/// <summary>Represents the availability status of a product.</summary>
public enum ProductStatus
{
    /// <summary>Product is available and visible to customers.</summary>
    Active = 0,

    /// <summary>Product is hidden from customers.</summary>
    Inactive = 1,

    /// <summary>Product is visible but cannot be ordered.</summary>
    OutOfStock = 2
}
