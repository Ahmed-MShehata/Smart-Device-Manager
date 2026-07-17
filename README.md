# Smart Device Manager (SDM)

Smart Device Manager (SDM) is a modern Windows-based maintenance platform designed to help users monitor, maintain, and troubleshoot their computers.

The project follows a Clean Architecture approach with an Offline-First philosophy, allowing customers to continue using most features even without an internet connection.

---

## Features

### Customer Application

- Device Information Scanner
- Device Health Monitoring
- Offline Diagnostic Engine
- Software Package Manager
- System Components Installer
- Product Catalog
- Shopping Cart
- Order Requests
- Notifications
- Local Settings

---

### Admin Application

- Dashboard
- Product Management
- Software Package Management
- System Components Management
- Diagnostic Rules Management
- Order Management
- Notifications
- Logs
- System Settings

---

## Technology Stack

### Backend

- ASP.NET Core 9 Web API
- Entity Framework Core 9
- SQL Server
- SignalR
- Serilog

### Desktop

- WPF
- MVVM
- WebView2

### Local Storage

- SQLite

---

## Architecture

The project follows:

- Clean Architecture
- SOLID Principles
- Separation of Concerns
- Offline First
- API First

---

## Solution Structure

```text
SmartDeviceManager/

backend/
    SDM.API
    SDM.Application
    SDM.Domain
    SDM.Infrastructure

customer/
    SDM.CustomerApp

admin/
    SDM.AdminApp

docs/

prompts/
```

---

## Current Status

Current Sprint:

✅ Sprint 001 – Project Foundation

Completed:

- Clean Architecture
- Dependency Injection
- SQL Server Configuration
- SQLite Configuration
- JWT Foundation
- SignalR Foundation
- Logging Foundation
- WPF Project Structure
- .NET 9 LTS Migration

---

## Roadmap

- Sprint 002 – Domain Design ✅
- Sprint 003 – Domain Implementation
- Sprint 004 – Database Layer
- Sprint 005 – Authentication
- Sprint 006 – Device Scanner
- Sprint 007 – Health Engine
- Sprint 008 – Package Manager
- Sprint 009 – Diagnostic Engine
- Sprint 010 – Products & Orders

---

## License

This project is currently under active development.