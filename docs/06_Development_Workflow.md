# Smart Device Manager (SDM)

# Development Workflow

**Version:** 1.0

---

# Purpose

This document defines the standard development workflow for the Smart Device Manager project.

Every sprint must follow the same process to ensure consistency, quality, and maintainability.

---

# Development Lifecycle

Every Sprint follows the workflow below.

```
Planning
    ↓
Architecture Review
    ↓
Task Breakdown
    ↓
Implementation
    ↓
Build
    ↓
Testing
    ↓
Code Review
    ↓
Documentation Update
    ↓
Git Commit
    ↓
Git Push
    ↓
Sprint Approval
```

---

# Sprint Planning

Before starting any Sprint:

- Review project documentation.
- Review architecture.
- Review previous sprint.
- Define sprint goals.
- Define acceptance criteria.

---

# Architecture Review

Before implementation verify:

- Clean Architecture is respected.
- CQRS is respected.
- Repository Pattern is respected.
- Dependency Injection is respected.

No coding begins before architecture approval.

---

# Implementation Phase

Implementation rules:

- One feature at a time.
- Build frequently.
- Keep commits small.
- Follow Coding Standards.
- Keep architecture unchanged.

---

# Build Verification

Every Sprint must compile successfully.

Required:

✔ Build Success

✔ Zero Errors

✔ Zero Warnings

---

# Testing

Each feature should be tested.

Testing includes:

- Functional Testing
- Validation Testing
- Error Handling
- Integration Testing (when required)

---

# Code Review

Every completed Sprint must be reviewed.

Checklist:

- Architecture respected
- Clean code
- SOLID principles
- Naming conventions
- XML Documentation
- Logging
- Validation
- No duplicated code

---

# Documentation

After every Sprint:

Update documentation if architecture or behavior changes.

Documents include:

- Architecture
- Database
- API
- Project Decisions

---

# Git Workflow

Standard workflow:

```
git add .

git commit -m "feat(module): description"

git push
```

Commit Types:

- feat
- fix
- docs
- refactor
- style
- test
- chore

---

# Definition of Done (DoD)

A Sprint is considered complete only if:

- Feature implemented.
- Build successful.
- Zero errors.
- Zero warnings.
- Code reviewed.
- Documentation updated.
- Commit created.
- Code pushed.
- Sprint approved.

---

# Pull Request Checklist

Before merging:

✔ Architecture respected

✔ CQRS respected

✔ Repository Pattern respected

✔ Validation added

✔ Logging added

✔ No hardcoded values

✔ No business logic inside Controllers

✔ No business logic inside UI

✔ Documentation updated

---

# Quality Gates

Every Sprint must pass:

Architecture Review

↓

Build

↓

Testing

↓

Code Review

↓

Documentation Review

↓

Approval

---

# Sprint Approval

A Sprint is approved only after:

- Build passes
- Code review completed
- Documentation updated
- Git committed
- Git pushed

---

# Emergency Fixes

Emergency fixes must:

- Be isolated
- Be documented
- Be reviewed later
- Never bypass architecture

---

# Project Roles

## Product Owner

Responsible for:

- Features
- Priorities
- Final approval

---

## Solution Architect

Responsible for:

- Architecture
- Design decisions
- Documentation
- Code review

---

## Developer

Responsible for:

- Implementation
- Unit testing
- Bug fixing
- Following coding standards

---

# Continuous Improvement

After each Sprint:

- Review lessons learned.
- Improve documentation.
- Refactor if necessary.
- Update roadmap.

---

# Workflow Summary

Every Sprint follows the same sequence:

```
Plan

↓

Review

↓

Implement

↓

Build

↓

Test

↓

Review

↓

Document

↓

Commit

↓

Push

↓

Approve
```

Maintaining this workflow ensures that Smart Device Manager remains maintainable, scalable, and production-ready throughout its development lifecycle.