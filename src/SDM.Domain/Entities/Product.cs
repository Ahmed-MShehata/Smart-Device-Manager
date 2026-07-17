using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public string Brand { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public decimal Discount { get; private set; }
    public int Quantity { get; private set; }
    public string Warranty { get; private set; } = string.Empty;
    public string ImagePath { get; private set; } = string.Empty;
    public ProductStatus Status { get; private set; } = ProductStatus.Active;

    protected Product() { }

    public Product(string name, string category, string brand, string description,
        decimal price, decimal discount, int quantity, string warranty, string imagePath)
    {
        Name = name;
        Category = category;
        Brand = brand;
        Description = description;
        Price = price;
        Discount = discount;
        Quantity = quantity;
        Warranty = warranty;
        ImagePath = imagePath;
        Status = ProductStatus.Active;
    }

    public void Update(string name, string category, string brand, string description,
        decimal price, decimal discount, int quantity, string warranty, string imagePath)
    {
        Name = name;
        Category = category;
        Brand = brand;
        Description = description;
        Price = price;
        Discount = discount;
        Quantity = quantity;
        Warranty = warranty;
        ImagePath = imagePath;
    }

    public void SetStatus(ProductStatus status) => Status = status;
    public decimal FinalPrice => Price - (Price * Discount / 100);
}
