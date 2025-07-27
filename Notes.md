Overview
This solution includes:

A .NET 8 Web API that receives casino wager events and publishes them to a RabbitMQ queue.

A Background Consumer Service that processes messages from the queue and persists them in SQL Server 2022.

Two GET endpoints:

GET /api/player/{playerId}/casino – paginated wagers for a player.

GET /api/player/topSpenders – top spenders by total amount.

The project follows Clean Architecture principles with clear separation across:

Domain

Application

Infrastructure

API layers

I used Serilog for structured logging and Linq2Db for data access due to its performance and simplicity.

Known Issues
Failing Unit Test: A unit test is currently failing; however, functionality is working as expected.

DB Connection: Development was done on macOS, so Integrated Security (NT login) was not used.

HTTPS Dev Cert: Experienced issues installing the developer certificate on macOS, so the app was tested using HTTP.

Verification
Manual testing confirmed:

RabbitMQ message flow works end-to-end.

All wager events are correctly persisted in SQL Server.

Both GET endpoints return accurate and expected results under load (verified via Postman and direct SQL queries).

Structured logging and fallback error handlers have been added for resilience.

Production-Readiness Improvements
Move connection strings and sensitive data to Azure Key Vault.

Add full unit and integration test coverage.

Use FluentAssertions and Testcontainers for black-box and component-level testing.

Improve retry strategy and batch processing in the consumer for high-throughput scenarios.

Use Redis to cache results for top spenders for performance optimization.

General Notes
To run/test the solution:

Use docker-compose in the root to spin up SQL Server and RabbitMQ.

Run DatabaseGenerate.sql to set up the schema and seed initial data.

Logs are located at: src/OT.OnlineBetting.Api/logs.

Data integrity is enforced using foreign keys and constraints in the database.

Assumption: transactionId represents a unique transaction. Duplicate values are not allowed when creating a new wager.