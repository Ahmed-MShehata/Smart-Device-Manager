# Smart Device Manager (SDM)

# Coding Standards

Version: 1.0

---

# Purpose

This document defines the coding standards for the Smart Device Manager
project.

Every contributor must follow these rules to ensure consistency,
maintainability, readability and scalability.

---

# General Principles

- Write clean code.
- Keep methods small.
- Keep classes focused.
- Prefer readability over cleverness.
- Follow SOLID principles.
- Avoid duplicated code.

---

# Naming Convention

## Classes

Use PascalCase.

Example

ProductService

CreateOrderCommand

NotificationHub

---

## Interfaces

Always start with I.

Examples

IRepository

IUnitOfWork

IProductService

---

## Methods

PascalCase

Examples

CreateOrder()

GetProducts()

UploadPackage()

---

## Properties

PascalCase

Example

ProductName

CreatedAt

WarrantyMonths

---

## Private Fields

Prefix with underscore.

Example

_repository

_logger

_mapper

---

## Local Variables

camelCase

Example

product

orderItems

validationResult

---

## Constants

PascalCase

Example

MaxRetryCount

DefaultPageSize

---

# Folder Structure

Each feature should contain

Commands

Queries

Validators

DTOs

Mappings

Example

Application

Products

Commands

Queries

Validators

DTOs

---

# File Rules

One public class per file.

File name must match class name.

Enable Nullable.

Enable Implicit Usings.

---

# Async Rules

Always use async/await.

Never block threads.

Never use .Result.

Never use .Wait().

Always pass CancellationToken when possible.

---

# Dependency Injection

Never instantiate services manually.

Always use constructor injection.

Avoid Service Locator.

---

# Exception Handling

Never swallow exceptions.

Never expose internal exceptions.

Use Global Exception Middleware.

Log unexpected exceptions.

---

# Validation

Use FluentValidation.

Never validate inside Controllers.

Never validate inside Domain Entities.

---

# Logging

Use Serilog.

Log

Information

Warning

Error

Critical

Never log passwords.

Never log JWT tokens.

---

# Entity Rules

Use Guid IDs.

Use Value Objects where appropriate.

Avoid primitive obsession.

Keep business rules inside Domain.

---

# Repository Rules

Repositories should only access data.

Business logic belongs to Domain/Application.

---

# Controller Rules

Controllers must remain thin.

Controllers should

Receive Request

Call MediatR

Return Result

Nothing more.

---

# Result Pattern

Every command/query returns Result<T>

Example

Success

Data

Errors

Message

---

# Comments

Prefer self-documenting code.

Use XML Documentation for public members.

Avoid unnecessary comments.

---

# Code Formatting

4 Spaces

UTF-8

One blank line between methods.

Maximum line length: 120 characters.

---

# Nullable Reference Types

Enabled.

Avoid null whenever possible.

---

# LINQ

Prefer method syntax.

Avoid complex nested LINQ.

Project only required fields.

---

# Security

Never trust client input.

Always validate input.

Hash passwords using BCrypt.

Verify downloaded packages using SHA256.

---

# Testing Rules

Arrange

Act

Assert

Keep tests independent.

---

# Git Commit Convention

Examples

feat:

fix:

refactor:

docs:

test:

style:

chore:

Example

feat(products): add create product command

---

# Code Review Checklist

Before merging

✔ Build succeeds

✔ No warnings

✔ No duplicated code

✔ XML Documentation

✔ Validation exists

✔ Logging added

✔ Tests passed

✔ Naming conventions followed

✔ Clean Architecture respected

✔ CQRS respected

---

# Forbidden Practices

❌ Business logic inside UI

❌ Business logic inside Controllers

❌ Static helper classes for business rules

❌ EF Core inside Domain

❌ SQL inside Application

❌ Hardcoded configuration

❌ Magic strings

❌ Magic numbers

❌ Circular dependencies

---

# Summary

Following these coding standards ensures that Smart Device Manager remains maintainable, scalable and production-ready.