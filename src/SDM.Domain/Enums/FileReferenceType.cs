namespace SDM.Domain.Enums;

/// <summary>
/// Classifies the purpose of a file attached to a software package or system component.
/// </summary>
public enum FileReferenceType
{
    /// <summary>An executable installer (.exe / .msi).</summary>
    Installer = 1,

    /// <summary>A firmware image or update bundle.</summary>
    Firmware = 2,

    /// <summary>A runtime dependency required for execution (e.g., Visual C++ redistributable).</summary>
    RuntimeDependency = 3,

    /// <summary>A hardware driver package.</summary>
    DriverPackage = 4,

    /// <summary>A documentation, license, or other downloadable resource.</summary>
    Resource = 5
}
