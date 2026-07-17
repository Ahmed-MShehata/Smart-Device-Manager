# Smart Device Manager (SDM)

# API Design Document

**Version:** 2.0
**Framework:** ASP.NET Core 9
**Architecture:** REST API
**Pattern:** CQRS
**Authentication:** JWT
**Communication:** REST + SignalR

---

# API Overview

The SDM Backend exposes a secure RESTful API used by both the Customer
Desktop Application and the Admin Desktop Application.

The API is the only communication gateway between client applications
and the server.

Direct database access is strictly prohibited.

---

# API Principles

The API follows these principles:

- RESTful Design
- Clean Architecture
- CQRS
- Thin Controllers
- Result Pattern
- API Versioning
- Secure by Default
- Offline Synchronization Support

---

# Request Flow

Desktop Application

↓

Controller

↓

MediatR

↓

Command / Query

↓

Handler

↓

Repository

↓

Unit Of Work

↓

Database

---

# API Versioning

Base URL

```
/api/v1/
```

Future versions

```
/api/v2/
```

---

# Authentication

Only administrators authenticate.

Authentication Method

- JWT Bearer Token

Login Endpoint

```
POST
/api/v1/auth/login
```

Returns

- JWT Token
- Expiration
- User Information

---

# Authorization

Role Based Authorization.

Roles

- SuperAdmin
- Admin

Anonymous Endpoints

- Product Catalog
- Create Order
- Download Diagnostic Data
- Package List

Protected Endpoints

Everything related to administration.

---

# Standard Response Format

Every endpoint returns the same structure.

```json
{
    "success": true,
    "message": "Operation completed successfully.",
    "data": {},
    "errors": []
}
```

---

# HTTP Status Codes

200 OK

201 Created

204 No Content

400 Bad Request

401 Unauthorized

403 Forbidden

404 Not Found

409 Conflict

422 Validation Error

500 Internal Server Error

---

# Validation

Validation is implemented using FluentValidation.

Controllers never perform validation manually.

Benefits

- Clean Controllers
- Reusable Rules
- Easy Testing

---

# Exception Handling

Global Exception Middleware handles all exceptions.

Responses are converted into the standard Result format.

No internal exception details are exposed.

---

# Authentication APIs

## Login

POST

```
/api/v1/auth/login
```

Purpose

Authenticate administrators.

---

## Refresh Token

POST

```
/api/v1/auth/refresh
```

---

## Logout

POST

```
/api/v1/auth/logout
```

---

# Product APIs

## Get Products

GET

```
/api/v1/products
```

---

## Get Product

GET

```
/api/v1/products/{id}
```

---

## Create Product

POST

```
/api/v1/products
```

Admin Only

---

## Update Product

PUT

```
/api/v1/products/{id}
```

Admin Only

---

## Delete Product

DELETE

```
/api/v1/products/{id}
```

Admin Only

---

# Orders APIs

## Create Order

POST

```
/api/v1/orders
```

Customer Endpoint

Includes

- Customer Contact
- Device Reference
- Order Items

---

## Get Orders

GET

```
/api/v1/orders
```

Admin Only

---

## Update Order Status

PUT

```
/api/v1/orders/{id}/status
```

---

# Software Package APIs

## Get Packages

GET

```
/api/v1/packages
```

---

## Upload Package

POST

```
/api/v1/packages
```

---

## Update Package

PUT

```
/api/v1/packages/{id}
```

---

## Delete Package

DELETE

```
/api/v1/packages/{id}
```

---

# System Component APIs

GET

```
/api/v1/components
```

POST

```
/api/v1/components
```

PUT

DELETE

---

# Diagnostic APIs

## Categories

GET

```
/api/v1/diagnostic/categories
```

---

## Questions

GET

```
/api/v1/diagnostic/questions
```

---

## Sync

GET

```
/api/v1/diagnostic/sync
```

Downloads updated diagnostic rules.

---

# Notification APIs

GET

```
/api/v1/notifications
```

PUT

```
/api/v1/notifications/read
```

---

# Settings APIs

GET

```
/api/v1/settings
```

PUT

```
/api/v1/settings
```

---

# Offline Synchronization APIs

```
POST
/api/v1/sync/upload
```

Uploads pending offline operations.

---

```
GET
/api/v1/sync/download
```

Downloads updated data.

---

# Pagination

Large collections support pagination.

Query Parameters

```
?page=1&pageSize=20
```

---

# Filtering

Examples

```
?category=Printer

?status=Active

?search=HP
```

---

# Sorting

```
?sort=name

?sort=-price
```

---

# SignalR

SignalR Hub

```
/notificationHub
```

Used For

- Order Notifications
- Package Updates
- System Messages
- Admin Alerts

---

# Security

HTTPS Only

JWT Authentication

Role Authorization

Input Validation

SHA256 Package Verification

Rate Limiting (Future)

---

# API Rules

✔ Thin Controllers

✔ CQRS

✔ MediatR

✔ FluentValidation

✔ Repository Pattern

✔ Unit Of Work

✔ Result Pattern

✔ Dependency Injection

---

# Forbidden Practices

❌ Business Logic inside Controllers

❌ Direct SQL Queries inside Controllers

❌ Returning Entities directly

❌ Returning Exceptions to Clients

❌ Multiple Response Formats

---

# Future APIs

Future versions may expose

- Driver Repository API

- AI Assistant API

- Remote Support API

- Cloud Backup API

- Mobile API

---

# Summary

The SDM API is designed around Clean Architecture and CQRS principles.

It provides secure, versioned, scalable REST endpoints with unified responses, offline synchronization support, and real-time communication using SignalR.