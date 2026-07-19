# Smart Device Manager (SDM)

# System Architecture

**Version:** 1.0
**Architecture Style:** Clean Architecture
**Framework:** .NET 9

---

# Architecture Overview

Smart Device Manager follows a layered Clean Architecture.

The system is split into two frontend portals — Admin Portal and Customer Application — both backed by the same ASP.NET Core 9 REST API.

Each portal is a React 19 web application embedded inside a WPF desktop shell using WebView2.

---

# High-Level Architecture

```
+--------------------------------------------------+
|  Admin Portal  (WPF + WebView2 + React 19)       |
+--------------------------------------------------+

+--------------------------------------------------+
|  Customer Application  (WPF + WebView2 + React 19)|
+--------------------------------------------------+

                       │
                       ▼

+--------------------------------------------------+
|  ASP.NET Core 9 Web API                          |
+--------------------------------------------------+

                       │

+--------------------------------------------------+
|  Application Layer                               |
|  CQRS + MediatR + FluentValidation               |
+--------------------------------------------------+

                       │

+--------------------------------------------------+
|  Domain Layer                                    |
|  Entities + Value Objects + Business Rules       |
+--------------------------------------------------+

                       │

+--------------------------------------------------+
|  Infrastructure Layer                            |
|  EF Core + SQL Server + SQLite + JWT + SignalR   |
+--------------------------------------------------+

                       │

+--------------------------------------------------+
|  SQL Server / SQLite                             |
+--------------------------------------------------+
```

---

# Solution Structure

```
SDM.sln

- SDM.API
- SDM.Application
- SDM.Domain
- SDM.Infrastructure
- SDM.SharedKernel
- SDM.AdminApp         (WPF Host for Admin Portal)
- SDM.CustomerApp      (WPF Host for Customer Application)
```

---

# Layer Responsibilities

## Presentation Layer

Responsibilities

- User Interface
- User Interaction
- Navigation
- Display Data

Technology

- WPF (shell host)
- WebView2 (React renderer)
- React 19
- TypeScript

Rules

- No Business Logic
- No Database Access
- No EF Core
- No Validation Logic

---

## API Layer

Responsibilities

- REST Endpoints
- Authentication
- Authorization
- Request Routing

Rules

- Thin Controllers
- No Business Logic
- Use MediatR Only

---

## Application Layer

Responsibilities

- CQRS
- Commands
- Queries
- DTOs
- Validators
- Interfaces
- Mapping
- Result Pattern

Technology

- MediatR
- FluentValidation

Rules

- No EF Core
- No SQL Queries
- No UI Code

---

## Domain Layer

Responsibilities

- Entities
- Value Objects
- Enums
- Business Rules

Rules

- Pure C#
- No Framework Dependencies
- No EF Core
- No ASP.NET Core
- No MediatR

---

## Infrastructure Layer

Responsibilities

- Database
- Repositories
- JWT
- SignalR
- File Storage
- Logging

Technology

- EF Core
- SQL Server
- SQLite
- Serilog

---

# Dependency Rule

Presentation
↓

API
↓

Application
↓

Domain

Infrastructure depends on Application and Domain.

Domain depends on nothing.

---

# CQRS Architecture

Command → Handler → Repository → Database

Query → Handler → Repository → Database

Commands modify data.

Queries return data only.

---

# Portal Architecture

## Admin Portal

Pages:

- Login
- Dashboard
- Orders Management
- Software Management
- Knowledge Base Management
- Device Monitor
- Users
- Company Information
- Settings

---

## Customer Application

Pages:

- Dashboard
- Device Details
- Device Check
- Software Center
- Knowledge Base
- Orders
- Company Information

---

# Shared Component Rule

The following UI component is shared across both portals:

**DeviceHardwarePanel**

Used in:

- Customer Application → Device Details page
- Admin Portal → Device Monitor page

The component must not be duplicated. A single implementation is maintained and imported by both portals.

---

# SignalR Notification Architecture

When a customer submits an order:

1. The Customer Application posts the order to the API.
2. The API dispatches a SignalR event to all connected admin clients.
3. The WPF layer on the Admin application receives the event.
4. A native Windows notification is displayed.
5. Clicking the notification navigates to Orders Management.

There is no standalone Notifications page in either portal.

---

# Repository Pattern

Repositories abstract data access from the application layer.

Benefits

- Testability
- Maintainability
- Separation of Concerns

---

# Unit of Work

Coordinates repositories.

Ensures all database operations are committed together.

---

# Result Pattern

All commands and queries return a unified Result object.

Contains

- Success
- Data
- Errors
- Message

---

# Validation

Input validation uses FluentValidation.

Benefits

- Clean Controllers
- Reusable Rules
- Easy Testing

---

# Authentication

JWT-based authentication.

Only administrators authenticate.

Customers use the application without an account.

---

# Authorization

Role-Based Authorization.

Roles

- Super Admin
- Admin

---

# Logging

Serilog is used.

Logs include

- Errors
- Warnings
- Information
- Performance

---

# Error Handling

Global Exception Middleware

Result Pattern

Validation Errors

HTTP Status Codes

---

# Security

HTTPS

JWT

BCrypt

Role Authorization

Input Validation

---

# Architecture Rules

✔ Clean Architecture

✔ CQRS

✔ Repository Pattern

✔ Unit of Work

✔ Thin Controllers

✔ Result Pattern

✔ FluentValidation

✔ SignalR (order notifications only)

✔ Dependency Injection

✔ Shared UI Components between portals

---

# Forbidden Practices

❌ Business Logic inside UI

❌ Business Logic inside Controllers

❌ EF Core inside Domain

❌ SQL inside Application

❌ Static Business Classes

❌ Circular Dependencies

❌ Direct Database Access from Desktop Applications

❌ Duplicating shared UI components (e.g. DeviceHardwarePanel) between portals

---

# Future Architecture

Future versions may include

- AI Diagnostic Assistant
- Remote Support
- Cloud Synchronization
- Mobile Application
- Multi-language Support

Current architecture is designed to support these features without major redesign.