# Architectural Principles & Patterns

The Sunshine Mental Health Portal follows established enterprise software patterns to ensure maintainability, testability, and security.

---

## 1. Layered Clean Architecture

```text
Sunshine.App/
├── Controllers/       -> HTTP API Endpoints & Request/Response Contracts
├── Services/          -> Domain Logic, Validation, Business Rules & Transactions
├── Data/              -> EF Core Database Context & Relational Mappings
├── Models/            -> Domain Entities representing PostgreSQL tables
├── DTOs/              -> Data Transfer Objects isolating internal models from API contracts
└── wwwroot/           -> Static Frontend (HTML, CSS, Client JavaScript)
```

### Why Layered Architecture?
* **Separation of Concerns**: Controllers only handle HTTP parsing, status codes, and model validation. They delegate complex calculations and queries to domain services.
* **Database Isolation**: The frontend never directly talks to database tables; it communicates via strongly typed DTOs.
* **Testability**: Services can be tested independently of HTTP contexts using in-memory or mock database fixtures.

---

## 2. Stateless REST API & Authentication

* Sunshine uses **stateless REST APIs** communicating over JSON (`application/json`).
* Authentication is supported via:
  1. **HTTP-only Secure Cookies**: For seamless browser-based portal navigation.
  2. **Bearer JWT Tokens**: Attached via `Authorization: Bearer <token>` headers for mobile clients, curl scripts, and automated integration suites.
* The backend does not maintain in-memory session state, allowing the application to scale horizontally behind reverse proxies without session stickiness issues.

---

## 3. Database-First Pipeline with EF Core Code Mapping

* Database schema, constraints, and indexes are authored and version-controlled directly in pure SQL scripts (`sql/01_` through `sql/06_`).
* Entity Framework Core maps directly to these relational tables using `Npgsql.EntityFrameworkCore.PostgreSQL`.
* This hybrid approach ensures that database optimization (such as partial unique indexes and check constraints) is executed natively in PostgreSQL while providing strongly typed LINQ querying in C#.
