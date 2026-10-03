# Overall System Architecture Diagram

## Purpose
Visualizes the end-to-end multi-tier architecture of the Sunshine Mental Health Portal from the user's browser down to PostgreSQL storage.

## Diagram

```mermaid
flowchart TD
    User(["Client / User Device (Browser)"]) -->|HTTP/HTTPS JSON| WebServer["Kestrel Web Server (Port 5000)"]
    
    subgraph Frontend ["Client Presentation Tier (HTML5 / Material UI)"]
        Landing["Public Website (/)"]
        PatientPortal["Patient Care Portal (/patient)"]
        DoctorPortal["Doctor Practice Portal (/doctor)"]
        AdminPortal["Admin Console (/admin, /admin/login)"]
    end
    
    WebServer --> Frontend
    Frontend -->|REST API Requests| Controllers["ASP.NET Core Controllers"]
    
    subgraph Backend ["ASP.NET Core 10 Web Application"]
        Controllers --> AuthCtrl["AuthController"]
        Controllers --> PatientCtrl["PatientController"]
        Controllers --> DoctorCtrl["DoctorController"]
        Controllers --> AdminCtrl["AdminController"]
        Controllers --> PaymentCtrl["PaymentController"]
        
        AuthCtrl --> AuthService["AuthService (PBKDF2 / JWT)"]
        PatientCtrl --> AptService["AppointmentService"]
        DoctorCtrl --> AptService
        PaymentCtrl --> PayService["PaymentService (MFS Simulation)"]
        AdminCtrl --> AuditService["AuditService"]
        
        AptService --> DbContext["ApplicationDbContext (EF Core 10)"]
        PayService --> DbContext
        AuthService --> DbContext
        AuditService --> DbContext
    end
    
    subgraph DatabaseTier ["PostgreSQL 18 Database (sunshine_db)"]
        DbContext -->|Npgsql Provider SQL| PG["PostgreSQL Engine"]
        PG --> Tables[("16 Relational Tables")]
        PG --> PartialIndex[("Partial Unique Index (Double-Booking Prevention)")]
    end
```

## Explanation
1. **Client Tier**: Web browsers render responsive Material UI portals (Landing, Patient, Doctor, Admin) using pure HTML5, CSS3, and JavaScript.
2. **Kestrel Server**: ASP.NET Core's internal high-performance web server listens on port 5000 and terminates HTTP requests.
3. **Controller Tier**: Parses JSON payloads, validates Bangladesh mobile regexes, and checks role permissions.
4. **Service Tier**: Implements business rules (e.g. 15-minute advance booking hold, 45-minute BST slot calculation, and serializable transactions).
5. **Persistence Tier**: Entity Framework Core translates LINQ operations into SQL queries executed on PostgreSQL 18.
