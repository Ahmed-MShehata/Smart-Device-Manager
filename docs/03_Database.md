# Smart Device Manager (SDM)

## Database Design Document

## Version 1.0

# Database Strategy

The system uses two databases:

## Server Database

Technology: - SQL Server

Used for: - Admin management. - Products. - Orders. - Software
packages. - Notifications. - Diagnostic rules. - System configuration.

## Client Database

Technology: - SQLite

Used for: - Offline operations. - Device information. - Local
settings. - Diagnostic data. - Synchronization queue.

------------------------------------------------------------------------

# Main Server Entities

## AdminUsers

Stores administrator accounts.

Fields: - Id - Username - PasswordHash - Role - IsActive - CreatedAt

------------------------------------------------------------------------

## Products

Stores products available for customers.

Fields: - Id - Name - Category - Brand - Description - Price -
Discount - Quantity - Warranty - ImagePath - Status - CreatedAt

------------------------------------------------------------------------

## Orders

Stores customer orders.

Fields: - Id - CustomerName - PhoneNumber - Address - DeviceId -
Status - CreatedAt

------------------------------------------------------------------------

## OrderItems

Stores products inside orders.

Fields: - Id - OrderId - ProductId - Quantity - Price

Relationship: Order has many OrderItems.

------------------------------------------------------------------------

## SoftwarePackages

Stores application packages.

Fields: - Id - Name - Version - Category - Description - FilePath -
SilentInstallCommand - DetectionMethod - SHA256 - Size - Status

------------------------------------------------------------------------

## SystemComponents

Stores required Windows components.

Examples: - .NET Runtime - Visual C++ - DirectX

Fields: - Id - Name - Version - FilePath - SilentInstallCommand - SHA256

------------------------------------------------------------------------

## DiagnosticCategories

Stores problem categories.

Examples: - Overheating - Performance - Network - Windows Problems

------------------------------------------------------------------------

## DiagnosticQuestions

Stores MCQ questions.

Fields: - Id - CategoryId - QuestionText

------------------------------------------------------------------------

## DiagnosticChoices

Stores possible answers.

Fields: - Id - QuestionId - ChoiceText - ScoreValue

------------------------------------------------------------------------

## DiagnosticRules

Stores diagnosis logic.

Fields: - Id - RuleCondition - Result - Solution

------------------------------------------------------------------------

## Notifications

Stores system notifications.

Fields: - Id - UserType - Title - Message - IsRead - CreatedAt

------------------------------------------------------------------------

## Settings

Stores server configuration.

Examples: - WhatsApp number. - Support phone. - Company information.

------------------------------------------------------------------------

# Client SQLite Entities

## DeviceInfo

Stores scanned hardware information.

------------------------------------------------------------------------

## LocalSettings

Stores customer preferences.

------------------------------------------------------------------------

## LocalPackages

Stores installed package status.

------------------------------------------------------------------------

## DiagnosticCache

Stores offline diagnosis data.

------------------------------------------------------------------------

## SyncQueue

Stores operations waiting for server synchronization.

Fields: - Id - OperationType - Data - Status - CreatedAt

------------------------------------------------------------------------

# Database Rules

-   No direct database access from client.
-   All server operations go through API.
-   Client data must support offline mode.
-   Database changes require migration.
