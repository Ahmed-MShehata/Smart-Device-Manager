using Microsoft.Extensions.DependencyInjection;

namespace SDM.Application.Extensions;

/// <summary>
/// Retained for backward compatibility with the API's service registration pipeline.
/// Delegates to <see cref="DependencyInjection.AddApplicationServices"/>.
/// </summary>
[Obsolete("Use SDM.Application.DependencyInjection.AddApplicationServices() directly.")]
public static class ApplicationServiceExtensions
{
    /// <inheritdoc cref="DependencyInjection.AddApplicationServices"/>
    public static IServiceCollection AddApplicationServicesLegacy(this IServiceCollection services)
        => services.AddApplicationServices();
}
