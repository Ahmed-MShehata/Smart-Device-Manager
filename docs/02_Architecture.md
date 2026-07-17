# Smart Device Manager (SDM)

# System Architecture

**Version:** 2.0
**Architecture Style:** Clean Architecture
**Development Model:** Offline First
**Framework:** .NET 9

---

# Architecture Overview

Smart Device Manager follows a modern layered architecture based on
Clean Architecture principles.

The architecture separates responsibilities into independent layers,
making the system easier to maintain, test, and extend.

The project also follows the CQRS pattern to separate read operations
from write operations.

---

# High-Level Architecture

+------------------------------------------------------+
| Customer Desktop Application (WPF + WebView2)        |
+------------------------------------------------------+

+------------------------------------------------------+
| Admin Desktop Application (WPF + WebView2)           |
+------------------------------------------------------+

                       │
                       ▼

+------------------------------------------------------+
| ASP.NET Core 9 Web API                               |
+------------------------------------------------------+

                       │

+------------------------------------------------------+
| Application Layer                                    |
| CQRS + MediatR + FluentValidation                    |
+------------------------------------------------------+

                       │

+------------------------------------------------------+
| Domain Layer                                         |
| Entities + Value Objects + Business Rules            |
+------------------------------------------------------+

                       │

+------------------------------------------------------+
| Infrastructure Layer                                 |
| EF Core + SQL Server + SQLite + JWT + SignalR        |
+------------------------------------------------------+

                       │

+------------------------------------------------------+
| SQL Server / SQLite                                  |
+------------------------------------------------------+

---

# Solution Structure

SDM.sln

- SDM.API
- SDM.Application
- SDM.Domain
- SDM.Infrastructure
- SDM.SharedKernel
- SDM.CustomerApp
- SDM.AdminApp

---

# Layer Responsibilities

## Presentation Layer

Responsibilities

- User Interface
- User Interaction
- Navigation
- Display Data

Technology

- WPF
- WebView2
- MVVM

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

Command

↓

Handler

↓

Repository

↓

Database

----------------------------

Query

↓

Handler

↓

Repository

↓

Database

Commands modify data.

Queries return data only.

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

Input validation is implemented using FluentValidation.

Benefits

- Clean Controllers
- Reusable Rules
- Easy Testing

---

# Authentication

Authentication uses JWT.

Only administrators authenticate.

Customers use the application without creating an account.

---

# Authorization

Role-Based Authorization.

Roles

- Super Admin
- Admin

---

# Offline First

The application always works offline.

SQLite stores:

- Device Information
- Local Settings
- Cached Diagnostics
- Pending Synchronization

Synchronization occurs automatically once Internet becomes available.

---

# SignalR

Used for

- Notifications
- Order Updates
- System Messages

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

SHA256 Package Verification

---

# Architecture Rules

✔ Clean Architecture

✔ CQRS

✔ Repository Pattern

✔ Unit of Work

✔ Offline First

✔ Thin Controllers

✔ Result Pattern

✔ FluentValidation

✔ SignalR

✔ Dependency Injection

---

# Forbidden Practices

❌ Business Logic inside UI

❌ Business Logic inside Controllers

❌ EF Core inside Domain

❌ SQL inside Application

❌ Static Business Classes

❌ Circular Dependencies

❌ Direct Database Access from Desktop Applications

---

# Future Architecture

Future versions may include

- AI Assistant
- Driver Repository
- Mobile Application
- Cloud Synchronization
- Remote Support

Current architecture is designed to support these features without major redesign.