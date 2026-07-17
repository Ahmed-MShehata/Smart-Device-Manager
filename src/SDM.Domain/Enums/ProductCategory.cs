namespace SDM.Domain.Enums;

/// <summary>Represents the hardware category of a product.</summary>
public enum ProductCategory
{
    /// <summary>Random Access Memory modules.</summary>
    RAM = 0,

    /// <summary>Central Processing Units.</summary>
    CPU = 1,

    /// <summary>Graphics Processing Units.</summary>
    GPU = 2,

    /// <summary>Storage devices (SSD, HDD, NVMe).</summary>
    Storage = 3,

    /// <summary>Motherboards.</summary>
    Motherboard = 4,

    /// <summary>Power Supply Units.</summary>
    PSU = 5,

    /// <summary>Cooling solutions (fans, liquid cooling).</summary>
    Cooling = 6,

    /// <summary>PC cases and chassis.</summary>
    Case = 7,

    /// <summary>Peripherals (keyboard, mouse, monitor, headset).</summary>
    Peripheral = 8,

    /// <summary>Networking hardware (routers, adapters, switches).</summary>
    Networking = 9,

    /// <summary>Miscellaneous or uncategorized hardware.</summary>
    Other = 10
}
