namespace SDM.Domain.Enums;

/// <summary>
/// Classifies a software entry as either an installable application
/// or a hardware driver. Used in the unified Software Management page.
/// </summary>
public enum SoftwareCategory
{
    Application = 1,
    Driver = 2
}
