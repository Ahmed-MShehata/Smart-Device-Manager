# Smart Device Manager (SDM)

## System Architecture Document

## Version 1.0

## Architecture Overview

The system follows a client-server architecture with Offline First
support.

Components: - Customer Desktop Application. - Admin Desktop
Application. - ASP.NET Core Backend API. - SQL Server. - SQLite Local
Storage. - SignalR Notifications.

## Customer Application

Technology: - WPF. - WebView2. - MVVM. - SQLite.

Responsibilities: - Device scanning. - Health analysis. - Offline
diagnosis. - Software installation. - Synchronization.

## Backend

Technology: - ASP.NET Core Web API. - Clean Architecture. - Entity
Framework Core. - SQL Server. - SignalR.

Layers: - Domain. - Application. - Infrastructure. - API.

## Engines

### Device Scanner Engine

Collects hardware and operating system information.

### Health Engine

Analyzes device status and generates warnings.

### Diagnostic Engine

Uses offline MCQ rules and stored solutions.

### Package Manager Engine

Handles software packages, verification, and silent installation.

### Sync Engine

Handles offline queue synchronization.

### Notification Engine

Handles in-app and Windows notifications.

## Data Storage

Server: - SQL Server.

Client: - SQLite.

## Security Rules

-   HTTPS communication.
-   JWT authentication.
-   Password hashing.
-   Role based authorization.
-   API is the only communication gateway.

## Architecture Rules

-   No business logic in UI.
-   Modules must remain independent.
-   Do not redesign architecture.
