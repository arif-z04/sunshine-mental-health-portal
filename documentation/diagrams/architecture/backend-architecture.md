# Backend Layered Architecture Diagram

## Purpose
Visualizes the internal component structure and dependency injection lifecycle of `server/Sunshine.App`.

## Diagram

```mermaid
flowchart LR
    Request["Incoming HTTP Request"] --> Middleware["ASP.NET Core Pipeline"]
    
    subgraph Pipeline ["Middleware Pipeline (Program.cs)"]
        Middleware --> StaticFiles["Static Files Middleware"]
        StaticFiles --> Routing["Routing Middleware"]
        Routing --> AuthN["Authentication (Cookie / JWT Bearer)"]
        AuthN --> AuthZ["Authorization (RBAC Policies)"]
    end
    
    AuthZ --> Controllers["Controller Layer"]
    
    subgraph ControllersLayer ["Controllers (Scoped)"]
        Controllers --> C1["PatientController"]
        Controllers --> C2["DoctorController"]
        Controllers --> C3["AdminController"]
        Controllers --> C4["PaymentController"]
    end
    
    ControllersLayer --> ServicesLayer["Domain Services (Scoped)"]
    
    subgraph ServicesLayer ["Business Domain Services"]
        S1["IAppointmentService -> AppointmentService"]
        S2["IPaymentService -> PaymentService"]
        S3["IAuthService -> AuthService"]
        S4["IAuditService -> AuditService"]
    end
    
    ServicesLayer --> DataAccess["Data Access Layer"]
    
    subgraph DataAccess ["EF Core Data Access"]
        DbContext["ApplicationDbContext"]
        Npgsql["Npgsql PostgreSQL Provider"]
    end
```

## Explanation
* **Scoped Lifetimes**: All controllers, domain services, and `ApplicationDbContext` instances are instantiated once per HTTP request and cleanly disposed at the end of the request cycle.
