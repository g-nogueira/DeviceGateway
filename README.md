THIS IS JUST A INTERVIEW CHALLENGE. TAKE EVERYTHING HERE WITH A GRAIN OF SALT

The Devices service is an API Gateway which provides endpoints for managing 1Global eSIM devices. 

The goal of this microservice is to be secure, scalable, efficient, and traceable, while maintaining a good implementation of a REST API and Domain Driven Design

## Tech Stack
- Framework: .NET 9
- DB: PostgreSQL
- Caching: Redis
- Input Validation: FluentValidation
- Domain Validation: Rich Domain Model
- Mapping: Mapperly
- Repos: EF Core
- Logging: Serilog
- Sink: Console
- Package Manager: Nugget
- Scalability: Dockerfile / Helm / K8s

## Architecture / Patterns
- Clean Architecture / DDD
  - Devices.Api
  - Devices.Application
  - Devices.Domain
  - Devices.Infrastructure
- CQRS Pattern
- REST API with support for RFC 7807 Problem Details

## Architecture Decisions
In order to have a fully functional microservice while simulating a high load environment, I decided to adopt the following architecture decisions:

- Use EF instead of Dapper, as the more recent versions of EF Core have improved drastically the performance and seems to be on par with Dapper, while still allowing to have a more maintainable codebase and avoiding the pitfalls of manually writing SQL queries.
- Use Serilog with a Console sink, as it's a more common practice in microservices, and allows to easily integrate with log aggregation tools like ELK or Grafana Loki, while still allowing to have a good logging experience during development and local testing.
- Use Mapperly instead of AutoMapper, as it's a source generator and allows to have a better performance and less memory allocation, while still providing a good mapping experience and avoiding the pitfalls of reflection-based mappers.
- Use FluentValidation for input validation, as it's a well established library with a good community and allows to have a clean and maintainable validation logic, while still providing a good experience for defining validation rules and error messages.
- Use a rich domain model for domain validation, as it allows to have a better separation of concerns and a more DDD approach

- Use RFC 7807 Problem Details for error handling, as it's the standard way to represent errors in a REST API and allows to have a consistent and structured error response
- Use the Results pattern for handling the outcome of business operations, as it allows to have a clear and consistent way to represent success and failure cases, while avoiding increased memory allocation and processing time that comes with exceptions. In this way, we also give space to use Exceptions for truly exceptional cases, such as infrastructure failures or unexpected errors.

- Use the Clean Architecture in conjunction with DDD as it allows to have a clear separation of concerns, a increased maintainability and scalability of the codebase, and a better testability of the business logic
- Use CQRS pattern to structure the service, as it allows to have a clear separation between read and write operations, increased scalability, and a better maintainability of the codebase

## AI Usages:
- Automated Code Review
- Tests Generation
- Rubber duck

## DB Design

