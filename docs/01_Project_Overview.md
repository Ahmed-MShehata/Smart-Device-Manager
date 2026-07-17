# Smart Device Manager (SDM)

# Project Overview

**Version:** 2.0  
**Status:** Active Development  
**Framework:** .NET 9  
**Architecture:** Clean Architecture  
**Development Model:** Offline First

---

# Project Vision

Smart Device Manager (SDM) is a modern Windows platform designed to help users monitor, maintain, diagnose, and manage their computers through an integrated desktop solution.

The platform combines device monitoring, health analysis, offline diagnostics, software installation, hardware product management, customer support, and order management into a single application.

The system is designed using modern software architecture principles to provide high performance, maintainability, scalability, and long-term extensibility.

---

# Objectives

The primary objectives of SDM are:

- Monitor Windows devices.
- Analyze device health.
- Diagnose common problems offline.
- Install essential software packages.
- Recommend hardware upgrades.
- Allow customers to order products.
- Provide real-time notifications.
- Support offline operation.
- Synchronize data automatically.

---

# Target Users

## Customer

Customers can use the application without creating an account.

Features include:

- Device Scanner
- Health Check
- Offline Diagnostics
- Software Installation
- Product Catalog
- Orders
- Notifications
- Settings

---

## Administrator

Administrators authenticate using secure credentials.

Features include:

- Product Management
- Software Package Management
- System Component Management
- Diagnostic Rule Management
- Order Management
- Notification Management
- Settings Management
- Dashboard

---

# Solution Components

The SDM platform consists of the following components:

## Customer Desktop Application

Responsibilities:

- Device Scanning
- Health Analysis
- Offline Diagnostics
- Package Installation
- Product Browsing
- Order Submission
- Synchronization

Technology:

- WPF
- WebView2
- MVVM
- SQLite

---

## Admin Desktop Application

Responsibilities:

- System Administration
- Product Management
- Package Management
- Order Management
- Diagnostic Management
- Notifications

Technology:

- WPF
- WebView2
- MVVM

---

## Backend API

Responsibilities:

- Business Logic
- Authentication
- Data Validation
- Database Operations
- Synchronization
- Notifications

Technology:

- ASP.NET Core 9
- Clean Architecture
- CQRS
- MediatR
- FluentValidation
- Entity Framework Core

---

# Technology Stack

## Frontend

- WPF
- WebView2
- MVVM

## Backend

- ASP.NET Core 9
- Clean Architecture
- CQRS
- MediatR
- FluentValidation
- Entity Framework Core 9

## Databases

- SQL Server
- SQLite

## Security

- JWT Authentication
- BCrypt Password Hashing

## Communication

- REST API
- SignalR

## Logging

- Serilog

---

# Core Principles

The system follows the following principles:

- Offline First
- Clean Architecture
- Separation of Concerns
- API First
- Security First
- Scalability
- Maintainability
- Modularity
- Reusability

---

# Functional Modules

## Device Scanner

Collects hardware and operating system information.

---

## Health Analyzer

Analyzes device status.

Calculates health metrics.

Generates recommendations.

---

## Diagnostic Engine

Provides offline troubleshooting using predefined diagnostic rules.

---

## Package Manager

Installs software silently.

Verifies package integrity.

Detects installed applications.

---

## Product Catalog

Displays available hardware products.

Provides recommendations.

Supports filtering and searching.

---

## Order Management

Allows customers to submit orders.

Allows administrators to manage orders.

---

## Notification Center

Displays local and server notifications.

Uses SignalR for real-time updates.

---

## Synchronization Engine

Synchronizes offline data with the server whenever an Internet connection is available.

---

# Version 1 Scope

The first release includes:

- Customer Desktop Application
- Admin Desktop Application
- Backend API
- Device Scanner
- Health Analyzer
- Diagnostic Engine
- Package Manager
- Product Catalog
- Order Management
- Notifications
- Offline Synchronization

---

# Future Enhancements

The following features are planned for future versions:

- AI Assistant
- Driver Repository
- Compatibility Engine
- Remote Support
- Cloud Backup
- Mobile Application
- Multi-language Support

---

# Project Goals

The SDM platform aims to provide:

- High Performance
- Secure Architecture
- Offline Availability
- Easy Maintenance
- Modular Design
- Extensible Architecture
- Modern User Experience

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

---

# Summary

Smart Device Manager is a production-ready Windows platform built using modern .NET technologies and Clean Architecture principles.

The system focuses on reliability, maintainability, scalability, offline capability, and long-term extensibility while providing an efficient experience for both customers and administrators.