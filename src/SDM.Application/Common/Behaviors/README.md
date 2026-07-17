# Pipeline Behaviors

This folder contains MediatR `IPipelineBehavior<TRequest, TResponse>` implementations.

Pipeline behaviors are cross-cutting concerns that wrap every command and query handler automatically — without modifying the handlers themselves.

---

## How It Works

Every `ICommand` / `IQuery` dispatched through MediatR passes through the registered pipeline in registration order:

```
Request
  → [LoggingBehavior]
    → [PerformanceBehavior]
      → [ValidationBehavior]
        → [Handler]
      ← Result
    ← Result
  ← Result
```

Behaviors are registered as open generics in `Application/DependencyInjection.cs`:

```csharp
cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
```

---

## Planned Behaviors

### 1. `ValidationBehavior<TRequest, TResponse>`

**Sprint:** 007  
**Purpose:** Intercepts every command and query before it reaches the handler.  
Runs all registered `IValidator<TRequest>` implementations (FluentValidation).  
Returns `Result.Failure(errors)` immediately if validation fails — the handler is never called.

**Dependency:** `FluentValidation`

---

### 2. `LoggingBehavior<TRequest, TResponse>`

**Sprint:** Future  
**Purpose:** Logs every incoming request name and its outcome (success/failure).  
Uses `ILogger<T>` from `Microsoft.Extensions.Logging`.  
Helps with request traceability without touching individual handlers.

**Dependency:** `Serilog` / `Microsoft.Extensions.Logging`

---

### 3. `PerformanceBehavior<TRequest, TResponse>`

**Sprint:** Future  
**Purpose:** Measures handler execution time using `Stopwatch`.  
Emits a warning log when execution exceeds a configurable threshold (e.g., 500 ms).  
Helps identify slow queries early in development.

**Dependency:** `Microsoft.Extensions.Logging`

---

### 4. `AuthorizationBehavior<TRequest, TResponse>`

**Sprint:** Future  
**Purpose:** Checks authorization rules before the handler executes.  
Works alongside policy-based authorization already configured in the API layer.  
Can enforce fine-grained permissions (e.g., `IAuthorizedRequest`) that go beyond JWT roles.

**Dependency:** `ICurrentUserService`

---

## Registration Location

All behaviors are registered in:

```
Application/DependencyInjection.cs
```

Inside the `AddApplicationServices` extension method, using:

```csharp
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
});
```

> **Note:** Order matters. Behaviors execute in registration order (outermost first).
