# Smart Device Manager (SDM)

# Database Design

**Version:** 2.0  
**Database Strategy:** Hybrid (Online + Offline)  
**ORM:** Entity Framework Core 9  
**Database Approach:** Code First

---

# Database Overview

Smart Device Manager uses a hybrid database architecture.

The system consists of two independent databases:

- SQL Server (Server Database)
- SQLite (Client Database)

This architecture enables Offline-First functionality while maintaining centralized server management.

---

# Database Architecture

+-----------------------------+
| SQL Server                  |
| Main Server Database        |
+-----------------------------+

            ▲
            │
      ASP.NET Core API

            ▼

+-----------------------------+
| SQLite                      |
| Local Client Database       |
+-----------------------------+

---

# Server Database

Technology

- SQL Server
- Entity Framework Core
- Code First
- Migrations

Responsibilities

- Products
- Orders
- Software Packages
- System Components
- Notifications
- Settings
- Diagnostic Rules
- Administrator Accounts

---

# Client Database

Technology

- SQLite

Responsibilities

- Offline Storage
- Device Information
- Cached Diagnostic Rules
- Installed Packages
- Local Settings
- Synchronization Queue

---

# Design Principles

- Code First
- Guid Primary Keys
- Auditable Entities
- Offline First
- Repository Pattern
- Unit of Work
- Data Integrity

---

# Domain Entities

## AdminUser

Stores administrator accounts.

Fields

- Id
- Username
- PasswordHash
- Role
- IsActive
- CreatedAt

---

## Product

Stores hardware products.

Fields

- Id
- Name
- Description
- Category
- Brand
- Price
- Discount
- Quantity
- WarrantyMonths
- ImageUrl
- Status
- CreatedAt

---

## Order

Stores customer orders.

Fields

- Id
- CustomerContact
- DeviceReference
- Status
- CreatedAt

---

## OrderItem

Stores ordered products.

Fields

- Id
- OrderId
- ProductId
- Quantity
- UnitPrice

---

## SoftwarePackage

Stores software packages.

Fields

- Id
- Name
- Version
- Category
- InstallerType
- SilentInstallCommand
- DetectionRule
- SHA256
- Size
- RequiresRestart
- Status

---

## SystemComponent

Stores Windows components.

Examples

- .NET Runtime
- Visual C++
- DirectX

---

## DiagnosticCategory

Groups diagnostic questions.

---

## DiagnosticQuestion

Stores MCQ questions.

---

## DiagnosticChoice

Stores available answers.

---

## DiagnosticRule

Contains diagnosis logic and recommended solutions.

---

## Notification

Stores notifications.

Fields

- Id
- UserType
- Title
- Message
- IsRead
- IsPinned
- CreatedAt

---

## Setting

Stores global system configuration.

Examples

- Company Information
- WhatsApp Number
- Support Phone
- Application Settings

---

# Value Objects

The Domain uses Value Objects for immutable business data.

Current Value Objects

- DeviceReference
- CustomerContact

Benefits

- Immutable
- Self-validating
- Rich Domain Model

---

# Enums

Common Enumerations include

- ProductCategory
- InstallerType
- OrderStatus
- NotificationType
- UserRole

---

# Relationships

Product

↓

OrderItem

↓

Order

----------------------------

DiagnosticCategory

↓

DiagnosticQuestion

↓

DiagnosticChoice

----------------------------

DiagnosticCategory

↓

DiagnosticRule

---

# Auditing

All auditable entities contain

- CreatedAt
- UpdatedAt
- CreatedBy
- UpdatedBy

---

# Client SQLite Tables

## DeviceInfo

Stores hardware information.

---

## LocalSettings

Stores customer preferences.

---

## LocalPackages

Stores installed software status.

---

## DiagnosticCache

Stores downloaded diagnostic rules.

---

## SyncQueue

Stores pending synchronization operations.

Fields

- Id
- OperationType
- Payload
- Status
- RetryCount
- CreatedAt

---

# Synchronization Strategy

The application works completely offline.

Whenever Internet becomes available:

1. Upload pending operations.
2. Download updated rules.
3. Download notifications.
4. Update products.
5. Refresh packages.

---

# Migration Strategy

Database changes are managed using

Entity Framework Core Migrations.

Rules

- Never edit migration files manually.
- Every schema change requires a migration.
- Database updates are applied through EF Core.

---

# Data Integrity Rules

- Guid IDs only
- Foreign Keys enforced
- Required fields validated
- Cascade delete only when necessary
- SHA256 verification for packages

---

# Security

Server database is never accessed directly.

Desktop applications communicate only through the Web API.

JWT protects all administrator endpoints.

SQLite stores only offline customer data.

---

# Performance

Optimizations include

- Indexed foreign keys
- AsNoTracking queries
- Pagination
- Projection using DTOs
- Lazy loading disabled
- Explicit loading when required

---

# Future Database Extensions

Future versions may include

- Driver Repository
- Cloud Synchronization
- AI Knowledge Base
- Remote Support History
- Device Compatibility Database