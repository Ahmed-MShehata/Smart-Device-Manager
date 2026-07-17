namespace SDM.Domain.Enums;

/// <summary>Represents the installer format for a software package.</summary>
public enum InstallerType
{
    /// <summary>Executable installer (.exe).</summary>
    EXE = 0,

    /// <summary>Windows Installer package (.msi).</summary>
    MSI = 1,

    /// <summary>Compressed archive that requires extraction (.zip).</summary>
    ZIP = 2
}
