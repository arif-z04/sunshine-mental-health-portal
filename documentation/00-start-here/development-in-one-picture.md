# Development in One Picture

Here is how all the technologies in the Sunshine repository connect together:

```text
+-------------------------------------------------------------------------+
|                              USER DEVICE                                |
|                                                                         |
|   +-----------------------------------------------------------------+   |
|   |                       Web Browser (Client)                      |   |
|   |                                                                 |   |
|   |   HTML5 (Structure)  +  CSS3 (Style)  +  JavaScript (Behavior)  |   |
|   |             [ Material UI White Healthcare Theme ]              |   |
|   +-----------------------------------------------------------------+   |
+-------------------------------------------------------------------------+
                                    |
                             HTTP / HTTPS JSON
                      (e.g., POST /api/auth/login)
                                    v
+-------------------------------------------------------------------------+
|                       OMARCHY LINUX SERVER HOST                         |
|                                                                         |
|   +-----------------------------------------------------------------+   |
|   |                Kestrel Web Server (Port 5000)                   |   |
|   |                 ASP.NET Core 10 Application                     |   |
|   |                                                                 |   |
|   |   1. Middleware: Authenticates JWT token / sunshine_token cookie|   |
|   |   2. Controllers: Receives request, validates input data        |   |
|   |   3. Services: Executes business rules (AppointmentService)     |   |
|   |   4. EF Core: Maps C# objects to SQL queries                    |   |
|   +-----------------------------------------------------------------+   |
|                                   |                                     |
|                              Npgsql TCP                                 |
|                                   v                                     |
|   +-----------------------------------------------------------------+   |
|   |                     PostgreSQL 18 Database                      |   |
|   |                      Catalog: sunshine_db                       |   |
|   |                                                                 |   |
|   |   - 16 Relational Tables (users, doctors, appointments, etc.)   |   |
|   |   - Partial Unique Indexes (Preventing double-booking)          |   |
|   |   - Check Constraints (BDT currency, phone regex, fee >= 0)     |   |
|   +-----------------------------------------------------------------+   |
+-------------------------------------------------------------------------+
```
