# SupportHub - Enterprise Help Desk API
SupportHub is a high-performance, scalable Ticket Management System built with .NET 8 using Clean Architecture and CQRS principles. It demonstrates professional software engineering practices for handling complex business logic and cross-cutting concerns.
# Architectural Overview
The project follows the Onion Architecture (Clean Architecture) to ensure a high degree of maintainability, testability, and independence from external infrastructures.
Domain: Enterprise business logic, Entities, and Enums.
Application: Use cases managed via CQRS (MediatR), FluentValidation, and AutoMapper.
Persistence: Data access logic using EF Core, Generic Repository Pattern, and SQL Server.
API: RESTful endpoints with Global Exception Handling, Versioning, and Serilog integration.
# Tech Stack & Patterns
Framework: .NET 8 (C# 12)
Messaging & CQRS: MediatR for decoupled communication.
Validation: FluentValidation with automatic Pipeline Behaviors.
Logging: Serilog with Structured Logging (Console, File, and SQL Server sinks).
Caching: Advanced In-Memory Caching with automated Cache Invalidation via Mediator Notifications.
Mapping: AutoMapper for clean DTO/Entity transformations.
Testing: Unit Tests using xUnit and Moq.
# Professional Features
Global Exception Handling: Centralized middleware that catches errors and returns standardized JSON responses (RFC 7807).
Caching Pipeline: Custom CachingBehavior that intercept queries to provide sub-1ms response times for cached data.
Transactional Consistency: Ensuring data integrity across complex command operations.
API Versioning: Support for v1 and v2 routes to maintain backward compatibility during updates.
Security: Ready for JWT Authentication integration with a decoupled security layer.
