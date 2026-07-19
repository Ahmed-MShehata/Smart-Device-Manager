using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SDM.API.Core;
using SDM.Application;
using SDM.Infrastructure.Extensions;
using SDM.Infrastructure.Hubs;
using Serilog;
using System.Text;

// ─── Bootstrap Logger ─────────────────────────────────────────────────────
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/sdm-.log", rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Smart Device Manager API");

    var builder = WebApplication.CreateBuilder(args);

    // ─── Serilog ───────────────────────────────────────────────────────────
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .WriteTo.Console()
        .WriteTo.File("logs/sdm-.log", rollingInterval: RollingInterval.Day));

    // ─── Application Layer ─────────────────────────────────────────────────
    builder.Services.AddApplicationServices();

    // ─── Infrastructure Layer (DB) ─────────────────────────────────────────
    builder.Services.AddInfrastructureServices(builder.Configuration);

    // ─── JWT Authentication ────────────────────────────────────────────────
    builder.Services.AddJwtAuthentication(builder.Configuration);

    // ─── SignalR ───────────────────────────────────────────────────────────
    builder.Services.AddSignalR();

    // ─── Controllers & Swagger ─────────────────────────────────────────────
    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<SDM.API.Filters.ConcurrencyExceptionFilter>();
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "Smart Device Manager API", Version = "v1" });
    });

    // ─── HTTPS Redirection ─────────────────────────────────────────────────
    // Port 7156 is declared explicitly so UseHttpsRedirection() can redirect
    // correctly even when the HTTP-only launch profile is active.
    // Kestrel binds both ports via appsettings.Development.json → Kestrel:Endpoints.
    builder.Services.AddHttpsRedirection(options =>
    {
        options.HttpsPort = 7156;
    });

    // ─── CORS ──────────────────────────────────────────────────────────────
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials()
                  .SetIsOriginAllowed(_ => true);
        });
    });

    // ─── Build ─────────────────────────────────────────────────────────────
    var app = builder.Build();

    // ─── Middleware Pipeline ───────────────────────────────────────────────
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.UseStaticFiles();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // ─── SignalR Hubs ──────────────────────────────────────────────────────
    app.MapHub<NotificationHub>("/hubs/notifications");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

// ─── JWT Extension (API composition root) ─────────────────────────────────
static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        var secretKey = jwtSection["SecretKey"]
            ?? throw new InvalidOperationException("JWT SecretKey is not configured.");
        var issuer = jwtSection["Issuer"] ?? "SDM.API";
        var audience = jwtSection["Audience"] ?? "SDM.Client";

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };

            // Allow SignalR to send JWT via query string
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        path.StartsWithSegments("/hubs"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.RequireSuperAdmin, policy => policy.RequireRole(Roles.SuperAdmin));
            options.AddPolicy(Policies.RequireAdmin, policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin));
            options.AddPolicy(Policies.RequireAnyAdmin, policy => policy.RequireRole(Roles.SuperAdmin, Roles.Admin, Roles.Support));
        });
        return services;
    }
}
