using SDM.Domain.Common;
using SDM.Domain.Enums;

namespace SDM.Domain.Entities;

public class SoftwarePackage : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Version { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string FilePath { get; private set; } = string.Empty;
    public string SilentInstallCommand { get; private set; } = string.Empty;
    public string DetectionMethod { get; private set; } = string.Empty;
    public string SHA256 { get; private set; } = string.Empty;
    public long Size { get; private set; }
    public PackageStatus Status { get; private set; } = PackageStatus.Active;

    protected SoftwarePackage() { }

    public SoftwarePackage(string name, string version, string category, string description,
        string filePath, string silentInstallCommand, string detectionMethod, string sha256, long size)
    {
        Name = name;
        Version = version;
        Category = category;
        Description = description;
        FilePath = filePath;
        SilentInstallCommand = silentInstallCommand;
        DetectionMethod = detectionMethod;
        SHA256 = sha256;
        Size = size;
        Status = PackageStatus.Active;
    }

    public void SetStatus(PackageStatus status) => Status = status;
}
