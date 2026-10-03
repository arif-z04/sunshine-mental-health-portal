# ☀️ Sunshine Mental Health Portal — Complete Beginner-to-Developer Guide

Welcome to the **Sunshine Mental Health Portal (Bangladesh Edition)** documentation system!

If you have never built a full-stack web application, never configured a relational database, or never used a Linux terminal before, **you are in the right place**. This documentation is written specifically to teach you how modern software works from absolute zero, using this production-ready healthcare application as your real-world learning laboratory.

---

## 🗺️ Master Curriculum Roadmap

Follow the numbered chapters in order:

```text
========================================================================================
STAGE 1: FOUNDATIONS (No Prior Experience Required)
========================================================================================
[00-start-here]                        -> Welcome, how to use this guide & first day setup
[01-computer-and-development-basics]   -> What is software, clients, servers, APIs & JSON
[02-omarchy-linux]                     -> Omarchy/Arch Linux terminal, pacman, systemctl & ports
[03-development-environment]           -> Installing .NET 10, PostgreSQL 18, Git & code editors

========================================================================================
STAGE 2: THE WEB PLATFORM
========================================================================================
[04-web-development-basics]            -> HTML5 structure, CSS3 custom properties & DOM JavaScript
[05-http-and-api]                      -> HTTP verbs, headers, status codes, cookies & REST flow

========================================================================================
STAGE 3: BACKEND & DATABASE ENGINEERING
========================================================================================
[06-csharp]                            -> C# 13 syntax, OOP, interfaces, async/await & DI
[07-aspnet-core]                       -> Kestrel, Program.cs, controllers, services & request lifecycle
[08-postgresql-and-sql]                -> Relational design, 16 tables, constraints & partial indexes
[09-entity-framework-core]             -> ApplicationDbContext, LINQ queries & serializable transactions

========================================================================================
STAGE 4: SECURITY & SYSTEM ARCHITECTURE
========================================================================================
[10-authentication-and-authorization]  -> PBKDF2 password hashing, JWT tokens & RBAC roles
[11-project-overview]                  -> Personas, Bangladesh requirements (BDT, BST) & system story
[12-project-architecture]              -> Clean layered architecture & request/database tracing
[13-project-features]                  -> Specialist search, 45-min BST slots, bKash MFS & CBT vault
[14-api-documentation]                 -> REST endpoint catalog, request/response DTOs & curl tests

========================================================================================
STAGE 5: PROFESSIONAL ENGINEERING PRACTICES
========================================================================================
[15-testing]                           -> xUnit (26 tests), bash E2E verification & sanity queries
[16-debugging]                         -> Browser DevTools (F12), diagnostic funnel & common error trees
[17-security]                          -> Healthcare privacy, IDOR defense, SQL injection & XSS/CSRF
[18-development-workflow]              -> Daily workflow, adding features/APIs/tables & tracing records
[19-deployment]                        -> Systemd service daemons, Nginx reverse proxy & Certbot SSL
[20-troubleshooting]                   -> Error reference for Linux, PostgreSQL, .NET, and browsers

========================================================================================
STAGE 6: REFERENCE, ROADMAPS & DIAGRAMS
========================================================================================
[21-reference]                         -> Master commands, environment variables, glossary & FAQ
[22-learning-path]                     -> 12-week junior developer progression roadmap
[diagrams/]                            -> Mermaid visual models (Architecture, ERD, UML, DFD, Sequences)
[DOCUMENTATION-QA.md]                  -> Complete verification audit report against actual codebase
========================================================================================
```

---

## 📚 Complete Chapter Index

| Chapter | Title & Description | Key Lessons |
| :--- | :--- | :--- |
| **[00-start-here](00-start-here/welcome.md)** | Start Here | [Welcome](00-start-here/welcome.md) • [How to Use](00-start-here/how-to-use-this-documentation.md) • [Beginner Guide](00-start-here/absolute-beginner-guide.md) • [Architecture Picture](00-start-here/development-in-one-picture.md) • [First Day Run](00-start-here/first-day.md) |
| **[01-basics](01-computer-and-development-basics/what-is-software.md)** | Computer & Web Basics | [What is Software](01-computer-and-development-basics/what-is-software.md) • [Programs](01-computer-and-development-basics/what-is-a-program.md) • [Websites](01-computer-and-development-basics/what-is-a-website.md) • [Three Tiers](01-computer-and-development-basics/frontend-backend-database.md) • [Client-Server](01-computer-and-development-basics/client-server.md) • [APIs](01-computer-and-development-basics/what-is-an-api.md) • [JSON](01-computer-and-development-basics/what-is-json.md) |
| **[02-omarchy](02-omarchy-linux/omarchy-for-beginners.md)** | Omarchy Linux | [Omarchy Overview](02-omarchy-linux/omarchy-for-beginners.md) • [Terminal Basics](02-omarchy-linux/terminal-basics.md) • [Filesystem](02-omarchy-linux/filesystem-basics.md) • [Permissions & Sudo](02-omarchy-linux/permissions.md) • [Pacman](02-omarchy-linux/pacman.md) • [Systemctl](02-omarchy-linux/systemctl.md) • [Journalctl](02-omarchy-linux/journalctl.md) • [Ports](02-omarchy-linux/ports.md) |
| **[03-environment](03-development-environment/prerequisites.md)** | Dev Environment Setup | [Prerequisites](03-development-environment/prerequisites.md) • [Git](03-development-environment/git.md) • [GitHub](03-development-environment/github.md) • [.NET 10 SDK](03-development-environment/dotnet-sdk.md) • [PostgreSQL 18](03-development-environment/postgresql.md) • [Editors](03-development-environment/code-editor.md) • [Project Setup](03-development-environment/project-setup.md) |
| **[04-web-dev](04-web-development-basics/html.md)** | Web Standards UI | [Semantic HTML5](04-web-development-basics/html.md) • [CSS Variables & Themes](04-web-development-basics/css.md) • [Modern JavaScript](04-web-development-basics/javascript.md) • [DOM](04-web-development-basics/dom.md) • [Events](04-web-development-basics/events.md) • [Forms](04-web-development-basics/forms.md) • [Fetch API](04-web-development-basics/fetch-api.md) • [Responsive CSS](04-web-development-basics/responsive-design.md) |
| **[05-http-api](05-http-and-api/http-from-zero.md)** | HTTP Protocol & REST | [HTTP From Zero](05-http-and-api/http-from-zero.md) • [Requests](05-http-and-api/requests.md) • [Responses](05-http-and-api/responses.md) • [Verbs](05-http-and-api/methods.md) • [Status Codes](05-http-and-api/status-codes.md) • [Headers](05-http-and-api/headers.md) • [Cookies](05-http-and-api/cookies.md) • [REST Architecture](05-http-and-api/rest-api.md) • [API Flow](05-http-and-api/api-flow.md) |
| **[06-csharp](06-csharp/csharp-from-zero.md)** | C# 13 Programming | [C# From Zero](06-csharp/csharp-from-zero.md) • [Variables & Types](06-csharp/variables.md) • [Conditions](06-csharp/conditions.md) • [Methods](06-csharp/methods.md) • [Classes & Objects](06-csharp/classes.md) • [Interfaces](06-csharp/interfaces.md) • [Async / Await](06-csharp/async-await.md) • [Dependency Injection](06-csharp/dependency-injection.md) |
| **[07-aspnet](07-aspnet-core/aspnet-from-zero.md)** | ASP.NET Core 10 | [ASP.NET From Zero](07-aspnet-core/aspnet-from-zero.md) • [Program.cs](07-aspnet-core/program-cs.md) • [Configuration](07-aspnet-core/configuration.md) • [DI Lifetimes](07-aspnet-core/dependency-injection.md) • [Middleware](07-aspnet-core/middleware.md) • [Controllers](07-aspnet-core/controllers.md) • [Services](07-aspnet-core/services.md) • [Request Lifecycle](07-aspnet-core/request-lifecycle.md) |
| **[08-database](08-postgresql-and-sql/database-from-zero.md)** | PostgreSQL 18 & SQL | [Databases From Zero](08-postgresql-and-sql/database-from-zero.md) • [SQL From Zero](08-postgresql-and-sql/sql-from-zero.md) • [16 Relational Tables](08-postgresql-and-sql/tables.md) • [Primary & Foreign Keys](08-postgresql-and-sql/primary-keys.md) • [Double-Booking Index](08-postgresql-and-sql/indexes.md) • [Transactions](08-postgresql-and-sql/transactions.md) • [Seed Pipeline](08-postgresql-and-sql/actual-project-sql.md) |
| **[09-ef-core](09-entity-framework-core/ef-core-from-zero.md)** | Entity Framework Core | [EF Core From Zero](09-entity-framework-core/ef-core-from-zero.md) • [ApplicationDbContext](09-entity-framework-core/dbcontext.md) • [DbSets](09-entity-framework-core/dbset.md) • [Navigation Properties](09-entity-framework-core/relationships.md) • [LINQ Queries](09-entity-framework-core/linq.md) • [Serializable Isolation](09-entity-framework-core/transactions.md) |
| **[10-auth](10-authentication-and-authorization/authentication-from-zero.md)** | Authentication & RBAC | [Authentication vs Authorization](10-authentication-and-authorization/authentication-from-zero.md) • [Login Flow](10-authentication-and-authorization/login.md) • [Salted PBKDF2](10-authentication-and-authorization/password-security.md) • [Roles Matrix](10-authentication-and-authorization/roles.md) • [IDOR Protection](10-authentication-and-authorization/protected-endpoints.md) |
| **[11-overview](11-project-overview/what-is-sunshine-portal.md)** | Sunshine Project Overview | [What is Sunshine](11-project-overview/what-is-sunshine-portal.md) • [Personas](11-project-overview/users.md) • [Bangladesh Context](11-project-overview/features.md) • [Tech Stack](11-project-overview/technology-stack.md) • [Repository Tree](11-project-overview/project-structure.md) • [The Whole System in One Story](11-project-overview/complete-system-flow.md) |
| **[12-architecture](12-project-architecture/architecture-from-zero.md)** | Software Architecture | [Architecture From Zero](12-project-architecture/architecture-from-zero.md) • [Frontend Tier](12-project-architecture/frontend-architecture.md) • [Backend Tier](12-project-architecture/backend-architecture.md) • [Database Tier](12-project-architecture/database-architecture.md) • [Complete Request Flow](12-project-architecture/complete-request-flow.md) |
| **[13-features](13-project-features/authentication.md)** | Feature Walkthroughs | [Authentication](13-project-features/authentication.md) • [Patient Care Portal](13-project-features/patient-system.md) • [Doctor Practice Portal](13-project-features/counselor-system.md) • [Appointment Engine](13-project-features/appointment-system.md) • [BST Dynamic Slots](13-project-features/scheduling-system.md) • [Resource Vault](13-project-features/resources.md) • [Admin Console](13-project-features/admin-system.md) |
| **[14-api-docs](14-api-documentation/api-from-zero.md)** | REST API Reference | [Endpoints Catalog](14-api-documentation/endpoints.md) • [Auth Headers](14-api-documentation/authentication.md) • [JSON Payloads](14-api-documentation/request-response.md) • [Error Formats](14-api-documentation/errors.md) • [Testing with cURL](14-api-documentation/testing-api.md) |
| **[15-testing](15-testing/testing-from-zero.md)** | Testing & Verification | [Testing From Zero](15-testing/testing-from-zero.md) • [xUnit Test Suite (26 tests)](15-testing/unit-testing.md) • [Master E2E Script (test_e2e_full.sh)](15-testing/e2e-testing.md) • [Database Sanity Queries](15-testing/database-testing.md) • [Manual UI Checklist](15-testing/ui-testing.md) |
| **[16-debugging](16-debugging/debugging-from-zero.md)** | Debugging & Diagnostics | [Debugging From Zero](16-debugging/debugging-from-zero.md) • [Browser DevTools (F12)](16-debugging/browser-devtools.md) • [Frontend Errors](16-debugging/frontend-debugging.md) • [API & Server Logs](16-debugging/backend-debugging.md) • [Decision Tree](16-debugging/common-errors.md) |
| **[17-security](17-security/security-from-zero.md)** | Security & Privacy | [Security From Zero](17-security/security-from-zero.md) • [Brute-Force Defense](17-security/authentication-security.md) • [SQL Injection Defense](17-security/sql-injection.md) • [XSS / CSRF](17-security/xss.md) • [IDOR Defense](17-security/idor.md) • [Patient Privacy](17-security/privacy.md) |
| **[18-workflow](18-development-workflow/how-to-read-the-project.md)** | Developer Workflows | [How to Read the Code](18-development-workflow/how-to-read-the-project.md) • [Add a Feature](18-development-workflow/how-to-add-a-feature.md) • [Add an API](18-development-workflow/how-to-add-an-api.md) • [Add a Table](18-development-workflow/how-to-add-a-table.md) • [Trace One Request](18-development-workflow/trace-one-request.md) • [Trace One Record](18-development-workflow/trace-one-database-record.md) • [First Code Change](18-development-workflow/first-feature-change.md) |
| **[19-deployment](19-deployment/deployment-from-zero.md)** | Production Deployment | [Deployment From Zero](19-deployment/deployment-from-zero.md) • [Build Binaries](19-deployment/build.md) • [Database Backups (pg_dump)](19-deployment/production-database.md) • [Systemd Daemon](19-deployment/deployment.md) • [Nginx Proxy](19-deployment/reverse-proxy.md) • [HTTPS SSL](19-deployment/https.md) |
| **[20-troubleshoot](20-troubleshooting/troubleshooting-from-zero.md)** | Troubleshooting Reference | [Troubleshooting From Zero](20-troubleshooting/troubleshooting-from-zero.md) • [Omarchy](20-troubleshooting/omarchy.md) • [PostgreSQL](20-troubleshooting/postgresql.md) • [.NET](20-troubleshooting/dotnet.md) • [EF Core](20-troubleshooting/ef-core.md) • [Frontend & Browser](20-troubleshooting/browser.md) |
| **[21-reference](21-reference/commands.md)** | Master Reference | [CLI Commands](21-reference/commands.md) • [Environment Variables](21-reference/environment-variables.md) • [Schema Quick Reference](21-reference/database-reference.md) • [API Quick Reference](21-reference/api-reference.md) • [File Reference](21-reference/file-reference.md) • [Glossary](21-reference/glossary.md) • [FAQ](21-reference/faq.md) |
| **[22-learning](22-learning-path/week-by-week-roadmap.md)** | Learning Roadmaps | [12-Week Roadmap](22-learning-path/week-by-week-roadmap.md) • [Linux Path](22-learning-path/linux-roadmap.md) • [Frontend Path](22-learning-path/frontend-roadmap.md) • [Backend Path](22-learning-path/backend-roadmap.md) • [Database Path](22-learning-path/database-roadmap.md) • [Next Projects](22-learning-path/next-projects.md) |
| **[Visual Diagrams](diagrams/README.md)** | Visual Modeling Suite | [Diagram Comparison Guide](diagrams/comparison.md) • [Overall Architecture](diagrams/architecture/overall-architecture.md) • [Database ERD](diagrams/database/er-diagram.md) • [UML Class Diagram](diagrams/uml/class-diagram.md) • [UML Use Case](diagrams/uml/use-case-diagram.md) • [Sequences](diagrams/uml/sequence-diagrams.md) • [DFD Levels 0, 1, 2](diagrams/dfd/dfd-level-0.md) |
| **[QA Report](DOCUMENTATION-QA.md)** | Quality Assurance Audit | Comprehensive validation audit proving every link, command, table, API, and diagram matches the actual source code. |

---

## ⚡ 5-Minute Developer Quickstart

```bash
# 1. Initialize PostgreSQL database using the canonical SQL pipeline
sudo -u postgres psql -f sql/01_create_database.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/02_extensions.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/03_schema.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/04_indexes.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/05_constraints.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/06_seed_data.sql

# 2. Build the solution and run automated test suite
dotnet build Sunshine.slnx
dotnet test

# 3. Start local Kestrel server
cd server/Sunshine.App
dotnet run --urls "http://0.0.0.0:5000"

# 4. Open in your browser:
# Public Landing: http://localhost:5000/
# Patient Portal: http://localhost:5000/patient
# Doctor Portal:  http://localhost:5000/doctor
# Admin Console:  http://localhost:5000/admin (Login: http://localhost:5000/admin/login)
```

---

## 🔑 Demo Credentials (Password: `Password123!`)
* **Admin**: `admin@sunshine.org`
* **Specialist Doctor (Advance Policy)**: `dr.tanvir@sunshine.org`
* **Specialist Doctor (Post-Pay Policy)**: `dr.rafiq@sunshine.org`
* **Subscribed Patient**: `anika@example.com`
* **Standard Patient**: `sazzad@example.com`

---

## 🔄 Documentation Maintenance Policy
Whenever making changes to this codebase:
1. **Source Code is Ground Truth**: If code and documentation disagree, update the documentation.
2. **Database Changes**: Update `08-postgresql-and-sql/`, `09-entity-framework-core/`, and `diagrams/database/er-diagram.md`.
3. **API Changes**: Update `14-api-documentation/` and `21-reference/api-reference.md`.
4. **Testing Standards**: Keep the automated test counts updated in `15-testing/unit-testing.md`.
