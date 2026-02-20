THIS IS JUST A INTERVIEW CHALLENGE. TAKE EVERYTHING HERE WITH A GRAIN OF SALT

The Devices service is an API Gateway which provides endpoints for managing devices. 

The goal of this microservice is to be secure, scalable, efficient, and traceable, while maintaining a good implementation of a REST API and Domain Driven Design

## Tech Stack
- Framework: .NET 10
- DB: SQL Server
- Caching: Redis
- Input Validation: FluentValidation
- Domain Validation: Rich Domain Model
- Mapping: Mapperly
- Repos: Dapper
- Logging: Serilog
- Sink: ... Local Files(?)
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

  
## AI Usages:
- Automated Code Review
- Tests Generation
- Rubber duck