# Smart Device Manager (SDM)

# Project Overview

**Version:** 1.0
**Status:** Active Development
**Framework:** .NET 9
**Architecture:** Clean Architecture

---

# Project Vision

Smart Device Manager (SDM) is a modern Windows desktop platform designed to help IT support teams manage computers, software, hardware diagnostics, and customer service operations through two dedicated desktop applications.

The platform is split into two distinct portals:

- **Admin Portal** — for IT administrators to manage software, orders, customers, and company content.
- **Customer Application** — for customers to monitor their own device, install software, run diagnostics, and place orders.

Both applications share a common backend API and are hosted inside WPF using WebView2.

---

# Target Users

## Administrator

Administrators authenticate using secure credentials.

The Admin Portal includes:

- Dashboard
- Orders Management
- Software Management
- Knowledge Base Management
- Device Monitor
- Users
- Company Information
- Settings

---

## Customer

Customers use the Customer Application without creating an account and without a username or password.

On first launch, a simple onboarding form collects locally stored customer information:

- Full Name
- Phone Number
- WhatsApp Number (optional)
- Governorate
- Address

This information is saved locally on the customer's computer and is used to auto-fill order submissions. The customer may edit it at any time. This is NOT authentication.

The Customer Application includes:

- Dashboard
- Device Details
- Device Check
- Software Center
- Knowledge Base
- Orders
- Company Information

---

# Applications

## Admin Portal

A desktop-first React dashboard embedded inside WPF via WebView2.

The Admin Portal manages all operational and administrative functions.

---

## Customer Application

A desktop-first React application embedded inside WPF via WebView2.

Customers interact with their device data and the company's services through a clean, approachable interface.

Navigation uses a compact left sidebar appropriate for a Windows desktop application.

---

## Backend API

Responsibilities:

- Business Logic
- Authentication
- Data Validation
- Database Operations
- Real-time Notifications (SignalR)

Technology:

- ASP.NET Core 9
- Clean Architecture
- CQRS
- MediatR
- FluentValidation
- Entity Framework Core 9

---

# Technology Stack

## Frontend (Both Portals)

- React 19
- Vite
- TypeScript
- Tailwind CSS v4
- shadcn/ui

## Backend

- ASP.NET Core 9
- Clean Architecture
- CQRS
- MediatR
- FluentValidation
- Entity Framework Core 9

## Databases

- SQL Server (primary)
- SQLite (local device cache)

## Security

- JWT Authentication
- BCrypt Password Hashing

## Communication

- REST API
- SignalR (real-time order notifications)

## Logging

- Serilog

---

# Functional Modules

## Admin Portal Modules

### Dashboard

General statistics, recent orders, system status, and quick actions.

---

### Orders Management

Displays and manages all customer orders received from the Customer Application.

Each order contains:

- Customer Name
- Phone Number
- WhatsApp
- Address
- Requested Products
- Product Images
- Order Status
- Created Date

When a new order is submitted by a customer, SignalR sends a native Windows notification to the admin desktop application.

---

### Software Management

A unified page to manage both applications and drivers.

Each software entry contains:

- Name
- Category (Application or Driver)
- Version
- Description
- Icon
- Setup File
- Upload Date
- Updated Date

Admin workflow:

- **First time:** Add New Software — fills all fields and uploads the setup file.
- **Future versions:** Update Existing Software — upload a newer setup file only.

When updating, only the following fields change:

- Setup File
- Version (auto-extracted when possible, or entered manually)
- Updated Date

The following fields are preserved on update:

- Software ID
- Name
- Category
- Description
- Icon

No silent install configuration.

No detection rule configuration.

Customers run the standard setup wizard.

---

### Knowledge Base Management

Admins create troubleshooting articles that appear inside the Customer Knowledge Base.

Each article includes:

- Problem Name
- Description
- Problem Image
- YouTube Video URL
- Category
- Display Order
- Visible / Hidden control

---

### Device Monitor

Displays device hardware details using the exact same shared UI component (`DeviceHardwarePanel`) used by Customer Device Details.

The visual layout is identical in both portals. Only available admin actions differ.

Shows: CPU, GPU, RAM, Storage, Disk Health, Disk Temperature, Network, Displays, Motherboard, BIOS, Windows Version, Installed Drivers, and Real-time Usage.

---

### Users

Manage administrator accounts and access permissions.

---

### Company Information

Configure company profile data visible to customers.

Includes: Name, Logo, Phone, WhatsApp, Email, Website, Facebook, and Address.

---

### Settings

General application settings and environment configuration.

---

## Customer Application Modules

### Dashboard

Shows the current PC health summary.

Cards include:

- CPU Usage
- GPU Usage
- CPU Temperature
- GPU Temperature
- RAM Usage
- Disk Usage
- Device Status
- Last Diagnostic
- Quick Actions

---

### Device Details

Displays complete hardware information for the customer's own computer.

Sections:

- CPU (Generation, Cores, Threads, Clock Speed, Temperature, Usage)
- GPU (Name, VRAM, Temperature, Usage, Driver Version)
- RAM (Total, Used, Free, Usage)
- Storage (per physical disk: Name, Type, Health, Temperature, Total, Used, Free; per partition: Usage)
- Displays (Monitor Name, Resolution, Refresh Rate — supports multiple monitors)
- Network (IP, MAC, Upload, Download)
- Motherboard
- BIOS
- Windows Version
- Installed Drivers

---

### Device Check

Runs a complete system diagnostic scan.

Checks include:

- CPU
- GPU
- RAM
- Storage
- Disk Health
- Windows Activation
- Missing Drivers
- High RAM Usage
- High CPU Usage
- Disk Almost Full
- Startup Programs
- Other system issues

After scanning, displays recommendations tailored to the findings.

---

### Software Center

Displays all software and drivers uploaded by the admin.

Each item shows one of three states:

- **Install** — launches the setup file
- **Update** — launches the newer setup file
- **Installed** — latest version already present

The system automatically compares the installed software version against the latest uploaded version.

No silent install. The customer runs the standard setup.

---

### Knowledge Base

Displays troubleshooting articles created by the admin.

Example articles:

- Missing Drivers
- Blue Screen
- Black Screen
- Windows Activation
- High RAM Usage
- No Internet
- Printer Problems
- Windows Update Errors
- GPU Driver Crash
- Disk Full
- Audio Problems

Each article shows:

- Problem Image
- Description
- A large YouTube button that opens the configured tutorial video

---

### Orders

A shopping experience for customers to browse and order products.

Steps:

1. Browse available products
2. Add to cart
3. Checkout

Checkout collects: Name, Phone, WhatsApp, and Address.

These values are automatically pre-filled from the locally stored customer profile set during onboarding. The customer may edit them before confirming.

The order is sent directly to Admin Orders Management.

When submitted, a SignalR event triggers a native Windows notification on the admin system.

---

### Company Information

Displays company profile data set by the admin.

Fields shown: Phone, WhatsApp, Address, Email, Website, and Facebook.

---

# Notifications

The Notifications center page has been removed from both portals.

Instead, when a customer submits a new order:

- A SignalR event is dispatched.
- A native Windows notification appears on the admin WPF application.
- The notification displays: "New Order Received", Customer Name, and Item Count.
- Clicking the notification opens the Orders Management page.

---

# Shared UI Components

The following UI components are defined once in the shared UI package and imported by both portals.

Duplicating any of these components between portals is forbidden.

| Component | Used In |
| :--- | :--- |
| `DeviceHardwarePanel` | Admin Device Monitor, Customer Device Details |
| `CpuCard` | DeviceHardwarePanel |
| `GpuCard` | DeviceHardwarePanel |
| `MemoryCard` | DeviceHardwarePanel |
| `DiskCard` | DeviceHardwarePanel |
| `TemperatureCard` | DeviceHardwarePanel |
| `NetworkCard` | DeviceHardwarePanel |
| `DisplayCard` | DeviceHardwarePanel |
| `StorageCard` | DeviceHardwarePanel |
| `SoftwareCard` | Customer Software Center |
| `CompanyInfoCard` | Customer Company Information |

---

# Core Principles

- Clean Architecture
- Separation of Concerns
- API First
- Security First
- Reusability (shared components across portals)
- Simplicity (no unnecessary modules or pages)
- Modularity

---

# Project Status

Current Development Phase:

- Foundation ✅
- Domain Design ✅
- Domain Implementation ✅
- Persistence Layer ✅
- Shared Kernel ⏳
- CQRS ⏳
- FluentValidation ⏳
- Authentication ⏳
- Admin Portal Frontend ⏳
- Customer Application Frontend ⏳

---

# Summary

Smart Device Manager is a two-portal Windows desktop platform built using .NET 9, React 19, and Clean Architecture principles.

The Admin Portal enables IT administrators to manage software, orders, knowledge base content, and customer interactions.

The Customer Application enables users to monitor their device health, run diagnostics, install software, access troubleshooting guides, and place orders.

Both applications share the same backend API and key UI components for consistency and maintainability.