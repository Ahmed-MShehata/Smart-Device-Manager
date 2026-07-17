# Smart Device Manager (SDM)

## API Design Document

## Version 1.0

# API Architecture

The system uses:

-   ASP.NET Core Web API.
-   REST architecture.
-   HTTPS communication.
-   Versioned endpoints.

Base format:

/api/v1/

------------------------------------------------------------------------

# Authentication APIs

## Login

POST

/api/v1/auth/login

Purpose: Admin authentication.

------------------------------------------------------------------------

# Products APIs

## Get Products

GET

/api/v1/products

Returns available products.

------------------------------------------------------------------------

## Create Product

POST

/api/v1/products

Admin only.

------------------------------------------------------------------------

## Update Product

PUT

/api/v1/products/{id}

------------------------------------------------------------------------

## Delete Product

DELETE

/api/v1/products/{id}

------------------------------------------------------------------------

# Orders APIs

## Create Order

POST

/api/v1/orders

Customer submits order.

Data: - Customer information. - Products. - Device information.

------------------------------------------------------------------------

## Get Orders

GET

/api/v1/orders

Admin only.

------------------------------------------------------------------------

# Software Package APIs

## Get Packages

GET

/api/v1/packages

Returns available packages.

------------------------------------------------------------------------

## Upload Package

POST

/api/v1/packages

Admin uploads software.

------------------------------------------------------------------------

# Diagnostic APIs

## Get Diagnostic Data

GET

/api/v1/diagnostic/categories

Returns offline diagnosis data.

------------------------------------------------------------------------

## Sync Diagnostic Rules

GET

/api/v1/diagnostic/sync

Updates local rules.

------------------------------------------------------------------------

# Notification APIs

## Get Notifications

GET

/api/v1/notifications

------------------------------------------------------------------------

# SignalR

Used for:

-   Real-time admin notifications.
-   Order notifications.
-   System messages.

------------------------------------------------------------------------

# API Rules

-   All responses use unified format.
-   Validate all inputs.
-   Use authentication where required.
-   Never expose database directly.
