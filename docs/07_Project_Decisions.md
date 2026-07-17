# Smart Device Manager (SDM)

# Architectural Decision Records (ADR)

**Version:** 1.0

---

# Purpose

This document records the architectural and technical decisions made throughout the Smart Device Manager (SDM) project.

Each decision includes:

- The decision itself.
- The reasoning behind it.
- The consequences of adopting it.

This document should be updated whenever a significant architectural decision is made.

---

# ADR-001

## Title

Use .NET 9

### Status

Accepted

### Decision

The backend will be developed using ASP.NET Core 9.

### Reason

- Latest stable framework
- High performance
- Long-term maintainability
- Modern C# language features

### Consequences

- Better performance
- Access to the latest .NET ecosystem
- Easier future upgrades

---

# ADR-002

## Title

Adopt Clean Architecture

### Status

Accepted

### Decision

The project follows Clean Architecture.

### Reason

- Separation of concerns
- Testability
- Scalability
- Maintainability

### Consequences

- Independent layers
- Easier feature expansion
- Reduced coupling

---

# ADR-003

## Title

Use CQRS + MediatR

### Status

Accepted

### Decision

Commands and Queries are separated using MediatR.

### Reason

- Better organization
- Single responsibility
- Easier maintenance
- Cleaner codebase

### Consequences

- More classes
- Simpler business logic
- Better scalability

---

# ADR-004

## Title

Repository + Unit of Work

### Status

Accepted

### Decision

Data access is abstracted through repositories and coordinated using Unit of Work.

### Reason

- Consistent data access
- Easier testing
- Clear transaction boundaries

### Consequences

- Centralized persistence
- Improved maintainability

---

# ADR-005

## Title

Offline First

### Status

Accepted

### Decision

The application must continue functioning when internet connectivity is unavailable.

### Reason

- Improve user experience
- Reduce service interruptions
- Support unstable network environments

### Consequences

- SQLite local database
- Synchronization mechanism
- Conflict handling

---

# ADR-006

## Title

Hybrid Database Strategy

### Status

Accepted

### Decision

Use SQL Server on the server and SQLite on desktop clients.

### Reason

- SQL Server for central storage
- SQLite for local offline storage

### Consequences

- Synchronization layer required
- Better application responsiveness

---

# ADR-007

## Title

Customer Application Without Authentication

### Status

Accepted

### Decision

Customers do not create accounts or sign in.

### Reason

- Reduce complexity
- Faster user experience
- Lower support requirements

### Consequences

- Customers provide contact information only when requesting a service or placing an order.
- Administrative features remain protected by authentication.

---

# ADR-008

## Title

JWT Authentication for Administrators

### Status

Accepted

### Decision

Only administrators authenticate using JWT.

### Reason

- Secure API access
- Stateless authentication
- Easy role-based authorization

### Consequences

- Protected administration endpoints
- Role-based permissions

---

# ADR-009

## Title

Real-Time Notifications Using SignalR

### Status

Accepted

### Decision

SignalR is used for real-time communication.

### Reason

- Instant notifications
- Better user experience
- Reduced polling

### Consequences

- Notification Hub
- Real-time updates

---

# ADR-010

## Title

FluentValidation

### Status

Accepted

### Decision

All request validation is implemented using FluentValidation.

### Reason

- Clean controllers
- Reusable validation rules
- Better separation of concerns

### Consequences

- Validation classes for every request
- Consistent validation behavior

---

# ADR-011

## Title

Serilog for Logging

### Status

Accepted

### Decision

Use Serilog as the primary logging framework.

### Reason

- Structured logging
- Better diagnostics
- Multiple log sinks

### Consequences

- Centralized logging
- Easier troubleshooting

---

# ADR-012

## Title

No AI in Version 1

### Status

Accepted

### Decision

Artificial Intelligence features are postponed.

### Reason

- Reduce initial complexity
- Focus on core functionality
- Faster delivery

### Consequences

- Stable first release
- AI reserved for future versions

---

# ADR-013

## Title

No Microservices

### Status

Accepted

### Decision

The system will remain a Modular Monolith.

### Reason

- Easier development
- Easier deployment
- Lower infrastructure cost

### Consequences

- Simpler architecture
- Easier debugging

---

# ADR-014

## Title

Git Operations Ownership

### Status

Accepted

### Decision

Git operations are performed only by the Product Owner.

### Reason

To maintain full control over the repository history and ensure that every change is manually reviewed before publication.

### Consequences

The developer (including AI assistants such as Claude) must never perform:

- git add
- git commit
- git push
- git merge
- git rebase
- git checkout
- git branch
- Git history modifications

The developer's responsibility ends after:

- Implementation
- Successful Build
- Zero Errors
- Zero Warnings
- Self Review

The Product Owner is solely responsible for:

- Code Review Approval
- Git Commit
- Git Push
- Branch Management
- Release Approval

---

# ADR-015

## Title

Documentation First

### Status

Accepted

### Decision

Architecture and documentation must be updated before implementing major features.

### Reason

- Better planning
- Shared understanding
- Easier onboarding

### Consequences

- Documentation becomes the project's source of truth.
- Major architectural changes require documentation updates before implementation.

---

# Future Decisions

Future ADRs may include:

- Cloud Deployment Strategy
- Automatic Update System
- Plugin Architecture
- Mobile Companion Application
- AI Integration
- Driver Recommendation Engine
- Telemetry Strategy

---

# Summary

Architectural Decision Records (ADRs) document the rationale behind important technical choices made during the development of Smart Device Manager.

Maintaining this document ensures consistency, preserves project knowledge, and helps future contributors understand why each architectural decision was made.