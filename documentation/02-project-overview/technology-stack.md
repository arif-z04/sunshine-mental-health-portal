# Technology Stack Reference

This project purposefully uses a modern, lightweight, and dependency-conscious technology stack.

---

## 1. Backend Stack

* **Programming Language**: C# 13 (.NET 10).
* **Framework**: ASP.NET Core 10 Web API.
* **Object-Relational Mapping (ORM)**: Entity Framework Core 10 (`Microsoft.EntityFrameworkCore`).
* **PostgreSQL Provider**: `Npgsql.EntityFrameworkCore.PostgreSQL` (Version 10.0.0-preview).
* **Authentication**: `Microsoft.AspNetCore.Authentication.JwtBearer` with HMAC-SHA256 signature verification.
* **Password Hashing**: Cryptographic PBKDF2 using `HMACSHA256` with 100,000 iterations and 128-bit cryptographically secure random salt.

---

## 2. Frontend Stack

* **Markup**: Semantic HTML5 with accessibility ARIA tags.
* **Styling**: Pure CSS3 structured with custom properties (CSS variables) implementing a clean white **Material Design / Material UI** aesthetic.
* **Scripting**: Pure modern JavaScript (ES6+ `async`/`await`, `fetch` API, DOM manipulation).
* **Icons**: Google Material Symbols / Material Icons loaded via official web font.
* **External Frameworks**: **Zero bulky JavaScript frameworks** (No React, Angular, or Vue build pipelines required). This ensures instantaneous page loads, zero node_modules vulnerabilities, and simple maintenance on any Linux workstation.

---

## 3. Database & Storage

* **Database Engine**: PostgreSQL 18.
* **Extensions**:
  - `uuid-ossp`: For generating standard UUID identifiers.
  - `pgcrypto`: For cryptographic database operations.

---

## 4. Testing Frameworks

* **Unit & Integration Testing**: `xUnit` (`xunit.v3` / `xunit.runner.visualstudio`).
* **In-Memory Testing DB**: `Microsoft.EntityFrameworkCore.InMemory`.
* **End-to-End Testing**: Native Bash scripts utilizing `curl` and JSON assertion utilities.
