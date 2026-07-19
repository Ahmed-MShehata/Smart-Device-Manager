# Smart Device Manager (SDM)
# Official API Implementation Contract
**Version:** 1.0

This document is the official implementation contract between the Backend and Frontend teams. It maps exactly to Project Documentation Version 1.0 rules.

All API communication strictly uses JSON.

---

## 1. Global Specifications

### Standard API Response Wrapper
Every endpoint (both Admin and Customer) must return this exact structure.

**Success format:**
```json
{
  "success": true,
  "message": "Operation successful.",
  "data": { ... },
  "errors": null
}
```

**Error format:**
```json
{
  "success": false,
  "message": "Validation failed.",
  "data": null,
  "errors": [
    { "field": "CustomerPhone", "error": "Phone number is required." }
  ]
}
```

### HTTP Status Codes
| Code | Meaning | Usage |
| :--- | :--- | :--- |
| `200 OK` | Success | Reading data, successful updates |
| `201 Created` | Created | Successfully created a new resource (Orders, Software, etc.) |
| `204 No Content` | Success (Empty) | Resource deleted successfully |
| `400 Bad Request` | Client Error | Malformed JSON or unreadable request |
| `401 Unauthorized` | Auth Error | Missing or invalid JWT token |
| `403 Forbidden` | Role Error | Valid JWT, but lacking permissions |
| `404 Not Found` | Target Missing | ID not found in database |
| `422 Unprocessable` | Validation Error| Business rule or FluentValidation failure |
| `500 Server Error` | Backend Crash | Unhandled exception (returns standardized error wrapper) |

---

## 2. Common DTO Definitions

### `ProductDto`
```json
{
  "id": "uuid",
  "name": "string",
  "brand": "string",
  "description": "string",
  "price": 0.00,
  "imageUrl": "string",
  "status": "string (Active|Inactive)",
  "createdAt": "iso-date"
}
```

### `OrderDto`
```json
{
  "id": "uuid",
  "customerName": "string",
  "customerPhone": "string",
  "customerWhatsApp": "string",
  "customerGovernorate": "string",
  "customerAddress": "string",
  "status": "string (Pending|Processing|Shipped|Completed|Cancelled)",
  "createdAt": "iso-date",
  "products": [
    {
      "productId": "uuid",
      "name": "string",
      "quantity": 1,
      "price": 0.00,
      "imageUrl": "string"
    }
  ]
}
```

### `SoftwareDto`
```json
{
  "id": "uuid",
  "name": "string",
  "category": "string (Application|Driver)",
  "version": "string",
  "description": "string",
  "iconUrl": "string",
  "setupFileUrl": "string",
  "createdAt": "iso-date",
  "updatedAt": "iso-date"
}
```

### `KnowledgeArticleDto`
```json
{
  "id": "uuid",
  "problemName": "string",
  "description": "string",
  "problemImageUrl": "string",
  "youTubeVideoUrl": "string",
  "category": "string",
  "displayOrder": 0,
  "visible": true,
  "createdAt": "iso-date",
  "updatedAt": "iso-date"
}
```

### `CompanyDto`
```json
{
  "id": "uuid",
  "companyName": "string",
  "logoUrl": "string",
  "phone": "string",
  "whatsApp": "string",
  "email": "string",
  "website": "string",
  "facebook": "string",
  "address": "string",
  "isActive": true
}
```

### `UserDto`
```json
{
  "id": "uuid",
  "name": "string",
  "email": "string",
  "role": "string (Super Admin|Admin)",
  "createdAt": "iso-date"
}
```

---

## 3. Global Validation Rules

| Entity Field | Rule |
| :--- | :--- |
| `CustomerName` | Required. Max length: 100. |
| `CustomerPhone` | Required. Digits only. |
| `CustomerWhatsApp`| Optional. Digits only if provided. |
| `CustomerGovernorate`| Required. Must not be empty. |
| `CustomerAddress` | Required. Max length: 500. |
| `Software.Version` | Required. Semantic versioning preferred but string allowed. |
| `Software.Category` | Explicitly constrained to enum values: `Application`, `Driver`. |
| `Order.Status` | Exact match to defined statuses. Customer creates as `Pending`. |
| `File Uploads` | Must be processed via FormData (Multipart). Max size defined by backend. |

---

## 4. Admin Endpoints

### Authentication
**Authentication: ❌ Not Required for Login / Required for Logout**

#### POST `/api/v1/auth/login`
- **Purpose:** Issue JWT tokens.
- **Request:** `{ "email": "admin@sdm.com", "password": "***" }`
- **Response:** `200 OK`
```json
{
  "success": true,
  "data": { "token": "jwt...", "user": { "name": "Admin", "role": "Super Admin" } }
}
```

#### POST `/api/v1/auth/logout`
- **Purpose:** Invalidates the current session.
- **Request:** `{}`
- **Response:** `200 OK`

---

### Orders Management
**Authentication: ✔ Required**

#### GET `/api/v1/orders`
- **Purpose:** List all incoming orders.
- **Response:** `200 OK` → `OrderDto[]`

#### GET `/api/v1/orders/{id}`
- **Purpose:** Fetch order details.
- **Response:** `200 OK` → `OrderDto`
- **Error:** `404 Not Found`

#### PUT `/api/v1/orders/{id}/status`
- **Purpose:** Admin updates workflow state.
- **Request:** `{ "status": "Shipped" }`
- **Response:** `200 OK` → `OrderDto`

---

### Product Management (Inside Admin Orders Page)
**Authentication: ✔ Required**

#### GET `/api/v1/products`
- **Purpose:** Get full management catalog.
- **Response:** `200 OK` → `ProductDto[]`

#### POST `/api/v1/products`
- **Purpose:** Add product to catalog.
- **Request:** `FormData` (Name, Brand, Description, Price, Image)
- **Response:** `201 Created` → `ProductDto`

#### PUT `/api/v1/products/{id}`
- **Purpose:** Update product metadata.
- **Request:** `FormData` or JSON (Name, Brand, Price, Status)
- **Response:** `200 OK` → `ProductDto`

#### DELETE `/api/v1/products/{id}`
- **Purpose:** Hard/soft delete product.
- **Response:** `204 No Content`

---

### Software Management
**Authentication: ✔ Required**

#### GET `/api/v1/software`
- **Purpose:** Retrieve unified applications and drivers.
- **Response:** `200 OK` → `SoftwareDto[]`

#### POST `/api/v1/software`
- **Purpose:** Add new software.
- **Request:** `FormData` (Name, Category, Version, Description, Icon File, Setup File)
- **Response:** `201 Created` → `SoftwareDto`

#### PUT `/api/v1/software/{id}`
- **Purpose:** Update existing. Setup file replacement automatically updates `UpdatedAt`.
- **Request:** `FormData` (Name, Category, Version, Description, Icon, Optional: New Setup File)
- **Response:** `200 OK` → `SoftwareDto`

#### DELETE `/api/v1/software/{id}`
- **Purpose:** Remove software.
- **Response:** `204 No Content`

---

### Knowledge Base Management
**Authentication: ✔ Required**

#### GET `/api/v1/knowledge-base`
- **Purpose:** Retrieve all articles including hidden ones for admin view.
- **Response:** `200 OK` → `KnowledgeArticleDto[]`

#### POST `/api/v1/knowledge-base`
- **Purpose:** Create troubleshooting guide.
- **Request:** `CreateKnowledgeArticleDto` JSON or FormData
- **Response:** `201 Created` → `KnowledgeArticleDto`

#### PUT `/api/v1/knowledge-base/{id}`
- **Purpose:** Update guide details / visibility.
- **Request:** `UpdateKnowledgeArticleDto` 
- **Response:** `200 OK` → `KnowledgeArticleDto`

#### DELETE `/api/v1/knowledge-base/{id}`
- **Purpose:** Delete article.
- **Response:** `204 No Content`

---

### Users Management
**Authentication: ✔ Required**

#### GET `/api/v1/users`
- **Purpose:** List admins.
- **Response:** `200 OK` → `UserDto[]`

#### POST `/api/v1/users`
- **Purpose:** Create admin user.
- **Response:** `201 Created`

#### PUT `/api/v1/users/{id}`
- **Purpose:** Edit user role/name.
- **Response:** `200 OK`

#### DELETE `/api/v1/users/{id}`
- **Purpose:** Revoke user.
- **Response:** `204 No Content`

---

### Company Information
**Authentication: ✔ Required**

#### GET `/api/v1/company`
- **Purpose:** Get global company profile.
- **Response:** `200 OK` → `CompanyDto`

#### PUT `/api/v1/company`
- **Purpose:** Edit company profile.
- **Request:** `UpdateCompanyDto`
- **Response:** `200 OK` → `CompanyDto`

---

## 5. Customer Endpoints

**Authentication: ALL ENDPOINTS ❌ NOT REQUIRED (Anonymous Access)**

The Customer Application relies on public data endpoints and system-level diagnostic APIs.

#### GET `/api/v1/dashboard`
- **Purpose:** Retrieves centralized alerts or announcements for the customer.
- **Response:** `200 OK`

#### GET `/api/v1/device/details`
- **Purpose:** Retrieve cached/system details if backend is maintaining sync state. (Note: Highly dynamic data is fetched locally via WPF bindings, but API serves persisted device history if required).
- **Response:** `200 OK` → `DeviceDetailsDto`

#### POST `/api/v1/device/check`
- **Purpose:** Trigger or log a system diagnostic scan result to standard backend analytics.
- **Response:** `200 OK`

#### GET `/api/v1/software`
- **Purpose:** Retrieve all applications and drivers (Visible only).
- **Response:** `200 OK` → `SoftwareDto[]`

#### GET `/api/v1/knowledge-base`
- **Purpose:** Get specific visible troubleshooting articles.
- **Response:** `200 OK` → `KnowledgeArticleDto[]`

#### GET `/api/v1/knowledge-base/{id}`
- **Purpose:** Read individual article.
- **Response:** `200 OK` → `KnowledgeArticleDto`

#### GET `/api/v1/products`
- **Purpose:** Load active product catalog for shopping.
- **Response:** `200 OK` → `ProductDto[]`

#### POST `/api/v1/orders`
- **Purpose:** Submit order using locally saved Onboarding data.
- **Request:**
```json
{
  "customerName": "John Doe",
  "customerPhone": "01000000000",
  "customerWhatsApp": "01000000000",
  "customerGovernorate": "Cairo",
  "customerAddress": "123 Device St.",
  "products": [
    {
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 1
    }
  ]
}
```
- **Response:** `201 Created`
```json
{
  "success": true,
  "message": "Order placed! We'll contact you soon.",
  "data": { "orderId": "uuid" },
  "errors": null
}
```

---

## 6. SignalR Events

The system uses SignalR to stream real-time events to the Admin Portal, strictly replacing legacy polling notifications.

### Hub Endpoint
```
/notificationHub
```

### Event: `OrderCreated`

- **Publisher:** Backend API (fired upon successful `POST /api/v1/orders` completion).
- **Subscriber:** Admin Desktop Application WPF Shell / Zustand Store.
- **Trigger:** A customer finishes checkout and data hits the database.
- **Payload Schema:**
```json
{
  "orderId": "uuid",
  "customerName": "John Doe",
  "createdAt": "iso-date",
  "itemCount": 1
}
```
- **Admin Action:** State interceptor triggers native Windows notification and increments counter badge in the UI.
