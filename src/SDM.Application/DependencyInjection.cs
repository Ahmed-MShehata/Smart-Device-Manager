using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SDM.Application.Common.Behaviors;
using System.Reflection;

namespace SDM.Application;

/// <summary>
/// Dependency injection registration for the Application layer.
/// Call <see cref="AddApplicationServices"/> from the API or host project's
/// service registration pipeline.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all Application layer services into the DI container.
    /// </summary>
    /// <remarks>
    /// Registers:
    /// <list type="bullet">
    ///   <item><description>
    ///     <b>MediatR</b> — assembly scanning for all commands, queries, and handlers.
    ///   </description></item>
    ///   <item><description>
    ///     <b>ValidationBehavior</b> — intercepts every request and runs all registered
    ///     <c>IValidator&lt;T&gt;</c> implementations before the handler executes.
    ///   </description></item>
    ///   <item><description>
    ///     <b>FluentValidation validators</b> — all <c>AbstractValidator&lt;T&gt;</c>
    ///     implementations in this assembly are auto-registered.
    ///   </description></item>
    /// </list>
    ///
    /// Future pipeline behaviors (LoggingBehavior, PerformanceBehavior) will be
    /// added here in later sprints.
    /// </remarks>
    /// <param name="services">The service collection to register services into.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // ─── MediatR + Pipeline ────────────────────────────────────────────
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            // Validation runs before every handler.
            // Short-circuits with Result.Failure if any IValidator<TRequest> fails.
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));

            // Future behaviors (add in order, outermost first):
            // cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            // cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
        });

        // ─── FluentValidation ──────────────────────────────────────────────
        // Auto-registers all AbstractValidator<T> implementations in this assembly.
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
